using System.Reflection;
using System.Text;
using Asp.Versioning;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuizzWebApi.Configuration;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Data;
using QuizzWebApi.Models;
using QuizzWebApi.Repository;
using QuizzWebApi.Services.Health;
using Swashbuckle.AspNetCore.SwaggerUI;
using Serilog;

namespace QuizzWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
        builder.Services.AddScoped<ApiAuthFilter>();
        builder.Services.AddScoped<JwtTokenFilter>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        if (!builder.Environment.IsDevelopment())
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .MinimumLevel.Information()
                .WriteTo.Seq("http://api-seq:5341")
                .WriteTo.Console()
                .CreateLogger();
            builder.Host.UseSerilog();
        }

        builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<UserIdentityContext>();

        builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret"]!);
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JwtConfig:Audience"],
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        builder.Services.AddAuthorization();

        builder.Services.AddSwaggerGen(options =>
        {
            var securityScheme = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Id = "jwt_auth",
                    Type = ReferenceType.SecurityScheme
                }
            };
            options.AddSecurityDefinition("jwt_auth", new OpenApiSecurityScheme()
            {
                Name = "Bearer",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Description = "Specify the authorization token.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
            });
            // Make sure swagger UI requires a Bearer token specified
            var securityRequirements = new OpenApiSecurityRequirement()
            {
                { securityScheme, Array.Empty<string>() },
            };
            options.AddSecurityRequirement(securityRequirements);

            var scheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey",
                },
                In = ParameterLocation.Header
            };
            options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "CAN BE EMPTY!!!",
                Type = SecuritySchemeType.ApiKey,
                Name = "x-api-key",
                In = ParameterLocation.Header,
                Scheme = "ApiKeyScheme"
            });
            var requirement = new OpenApiSecurityRequirement()
            {
                { scheme, new List<string>() }
            };
            options.AddSecurityRequirement(requirement);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policyBuilder =>
            {
                policyBuilder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        var configuration = builder.Configuration;

        builder.Services
            .AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.UnsupportedApiVersionStatusCode = 501;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        var connectionString = configuration.GetConnectionString("DefaultConnection")!;

        #region DbContext

        builder.Services.AddDbContext<UserIdentityContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddDbContext<QuizContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddDbContext<QuizContextV2>(options =>
            options.UseNpgsql(connectionString));

        /*builder.Services.AddDbContext<IdentityContext>(options =>
            options.UseNpgsql(connectionString));*/

        #endregion

        builder.Services.AddHealthChecks()
            .AddNpgSql(connectionString)
            .AddCheck<RelationHeathCheck<QuizContext>>(nameof(QuizContext))
            .AddCheck<RelationHeathCheck<QuizContextV2>>(nameof(QuizContextV2))
            .AddCheck<RelationHeathCheck<UserIdentityContext>>(nameof(UserIdentityContext));
        //.AddCheck<RelationHeathCheck<IdentityContext>>(nameof(IdentityContext));

        //builder.Services.AddAuthorization();
        //builder.Services.AddAuthentication();

        var app = builder.Build();
        app.UseCors("AllowAll");

        //using (var scope = app.Services.CreateScope()) { }

        if (!app.Environment.IsDevelopment())
            app.UseSerilogRequestLogging();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();

            foreach (var description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = $"{description.GroupName}";
                options.SwaggerEndpoint(url, name);
            }

            options.Interceptors = new InterceptorFunctions
            {
                RequestInterceptorFunction =
                    "function (req) { if (!req.headers['x-api-key']) { req.headers['x-api-key'] = '46BB6D176C0A4B56BA67B6A65CEBDA75'; } return req; }"
            };
        });

        app.MapHealthChecks("api/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        //app.UseHsts();
        //app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        //CreateAdminUser(app).Wait();

        app.Run();
    }

    private static async Task CreateAdminUser(IApplicationBuilder app)
    {
        var serviceProvider = app.ApplicationServices;

        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        const string roleName = "Admin";
        var adminEmail = configuration["AdminUser:Email"];
        var adminLogin = configuration["AdminUser:Login"];
        var adminPassword = configuration["AdminUser:Password"];

        var roleExists = await roleManager.RoleExistsAsync(roleName);

        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        var user = await userManager.FindByEmailAsync(adminEmail);

        if (user == null)
        {
            user = new User()
            {
                UserName = adminLogin,
                Email = adminEmail,
            };
            await userManager.CreateAsync(user, adminPassword);

            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}