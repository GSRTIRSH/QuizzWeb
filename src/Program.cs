using Microsoft.EntityFrameworkCore;
using QuizzWebApi.Data;
using QuizzWebApi.Models;

namespace QuizzWebApi;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        
        //var port = Environment.GetEnvironmentVariable("PORT") ?? "5200";
        var port = "5200";


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        var configuration = builder.Configuration;

        builder.Services.AddDbContext<UserContext>(options => 
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
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        //app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}