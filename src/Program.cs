using Asp.Versioning;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuizzWebApi.Configuration;
using QuizzWebApi.Configuration.Filters;
using QuizzWebApi.Data;
using QuizzWebApi.Services.Health;

namespace QuizzWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //var port = Environment.GetEnvironmentVariable("PORT") ?? "5200";

        builder.Services.AddControllers();
        builder.Services.AddControllers(options => { options.Filters.Add(new MyFilter()); });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

        builder.Services.AddScoped<ApiAuthFilter>();

        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "",
                Type = SecuritySchemeType.ApiKey,
                Name = "x-api-key",
                In = ParameterLocation.Header,
                Scheme = "ApiKeyScheme"
            });

            var scheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey",
                },
                In = ParameterLocation.Header
            };

            var requirement = new OpenApiSecurityRequirement()
            {
                { scheme, new List<string>() }
            };

            options.AddSecurityRequirement(requirement);
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policyBuilder =>
            {
                policyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        var configuration = builder.Configuration;

        builder.Services
            .AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.UnsupportedApiVersionStatusCode = 501;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddHealthChecks()
            .AddNpgSql(connectionString)
            .AddCheck<RelationHeathCheck<QuizContext>>("QuizzesTable")
            .AddCheck<RelationHeathCheck<UserContext>>("UsersTable");

        builder.Services.AddDbContext<UserContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddDbContext<QuizContext>(options =>
            options.UseNpgsql(connectionString));

        //builder.Services.AddAuthorization();

        var app = builder.Build();
        app.UseCors("AllowAll");

        // Configure the HTTP request pipeline.

        if (app.Environment.IsDevelopment())
        {
        }

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
        });

        //check if api is working
        app.MapGet("api/", async c =>
        {
            c.Response.StatusCode = StatusCodes.Status200OK;
            c.Response.ContentType = "text/plain";
            await c.Response.WriteAsync("Api response");
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

        app.Run();
    }
}