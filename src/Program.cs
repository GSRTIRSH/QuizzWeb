using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using QuizzWebApi.Configure;
using QuizzWebApi.Data;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace QuizzWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //var port = Environment.GetEnvironmentVariable("PORT") ?? "520

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

        builder.Services.AddSwaggerGen(options =>
        {
            /*options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();
            options.CustomSchemaIds(type => type.FullName);*/
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

        builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.UnsupportedApiVersionStatusCode = 501;
            options.ReportApiVersions = true;
        }).AddMvc().AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        //builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddDbContext<UserContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDbContext<QuizContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // builder.WebHost.UseKestrel(
        //     serverOptions =>
        //     {
        //         serverOptions.ListenAnyIP(int.Parse(port));
        //     });

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
                //var name = description.GroupName.ToUpperInvariant();
                var name = $"QuizWebApi {description.GroupName}";
                options.SwaggerEndpoint(url, name);
            }
        });
        
        //check if api is working
        app.MapGet("api/", () => new StatusCodeResult(StatusCodes.Status200OK));

        //app.UseHttpsRedirection();

        //app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}