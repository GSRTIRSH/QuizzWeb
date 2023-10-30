using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//using CMS.Areas.Identity.Data;

namespace CMS;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("CMSIdentityDbContextConnection") ??
                               throw new InvalidOperationException(
                                   "Connection string 'CMSIdentityDbContextConnection' not found.");

        //builder.Services.AddDbContext<CMSIdentityDbContext>(options => options.UseSqlite(connectionString));

        /*builder.Services
            .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<CMSIdentityDbContext>();*/

        builder.Services.AddControllersWithViews();

        /*builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "Identity.Application";
        }).AddCookie("Cookies").AddIdentityCookies();*/

        /*builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultUI();*/
        /*builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<CMSIdentityDbContext>();*/
        
        builder.Services.AddRazorPages();

        /*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
                policy.RequireRole("Admin"));
        });*/

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        /*
        app.UseAuthentication();
        app.UseAuthorization();
        */

        app.MapRazorPages();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}