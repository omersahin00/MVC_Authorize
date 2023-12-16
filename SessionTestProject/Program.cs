using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using SessionTestProject.Concrete;

namespace SessionTestProject;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        //builder.Services.AddMvc(config =>
        //{
        //    var policy = new AuthorizationPolicyBuilder()
        //                .RequireAuthenticatedUser()
        //                .Build();
        //    config.Filters.Add(new AuthorizeFilter(policy));
        //});
        builder.Services.AddDbContext<SqlDbContext>();

        
        //builder.Services.ConfigureApplicationCookie(options =>
        //{
        //    options.ExpireTimeSpan = TimeSpan.FromDays(30); // 30 gün süreyle tarayıcı çerezini sakla
        //});
        


        //builder.Services.AddMvc();
        builder.Services.AddAuthentication(
            CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option => {
                option.LoginPath = "/Account/Login";
                //option.ExpireTimeSpan = TimeSpan.FromDays(30); // 30 gün süreyle oturumu açık tut
                //option.SlidingExpiration = true; // Kullanıcı etkileşiminde oturum süresini uzat
            }
        );



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

        app.UseAuthentication();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}

