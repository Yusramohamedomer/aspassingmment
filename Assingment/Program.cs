using Assingment.Data;
using Assingment.Data;
using Assingment.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        var _con = builder.Configuration.GetConnectionString("mydb");
        builder.Services.AddDbContext<PatientDbContext>(options => options.UseSqlServer(_con));
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(2);
            options.Cookie.Name = "Assingment_Cookie";
            options.Cookie.HttpOnly = false;
            options.Cookie.Path = "/";
            options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;
        });

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        {
            options.LoginPath = "/Account/login";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(3);
        });


       builder.Services.AddScoped<AppointmentRepository>();
        builder.Services.AddScoped<EmployeeRepository>();

        var app = builder.Build();


        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");
        });

        app.Run();
    }
}