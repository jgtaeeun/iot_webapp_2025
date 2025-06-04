using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyPortfolioWebApp.Models;

namespace MyPortfolioWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //db���� �ʱ�ȭ
            builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseMySql(
                    builder.Configuration.GetConnectionString("smartHomeConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("smartHomeConnection"))
                ));
            //asp.net core identity ����
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();   //����

            app.UseAuthentication(); //����
             
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
