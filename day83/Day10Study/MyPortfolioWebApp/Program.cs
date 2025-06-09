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
            // �� ���ø����̼� ���� ����
            var builder = WebApplication.CreateBuilder(args);

            // MVC ������ ���� ��Ʈ�ѷ� + �� ���� ���
            builder.Services.AddControllersWithViews();

            // �����ͺ��̽� ���� (MySQL)
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("smartHomeConnection"), // ���� ���ڿ�
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("smartHomeConnection")) // MySQL ���� ���� �ڵ� ����
                ));

            // ASP.NET Core Identity ���� (����� ����/���� ����)
            builder.Services.AddIdentity<CustomUser, IdentityRole>()   // ����� Ŭ������ ����(Role) ����
                .AddEntityFrameworkStores<ApplicationDBContext>()      // ����� ������ EF Core�� ����
                .AddDefaultTokenProviders();                           // �̸��� ���� �� ��ū ���� ���

            // ��й�ȣ ��å ���� (���� �䱸���� ��ȭ)
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 4;             // �ּ� ����: 4��
                options.Password.RequireNonAlphanumeric = false; // Ư������ ���ʿ�
                options.Password.RequireUppercase = false;       // �빮�� ���ʿ�
                options.Password.RequireLowercase = false;       // �ҹ��� ���ʿ�
                options.Password.RequireDigit = false;           // ���� ���ʿ�
            });

            // ���ø����̼� ��ü ����
            var app = builder.Build();

            // ���� ȯ���� �ƴ� ��� ���� ó�� ������ ����
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error"); // ����� ���� ���� �������� ���𷺼�
            }

            app.UseStaticFiles();   // ���� ����(css, js ��) ����

            app.UseRouting();       // ����� ���� Ȱ��ȭ

            app.UseAuthorization(); // ���� �˻� �̵����

            app.UseAuthentication(); // ����� ���� �̵���� (���� �߿�: ���� -> ���� �˻�)

            // �⺻ ����� ���� (�⺻��: HomeController�� Index �׼�)
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // ���ø����̼� ����
            app.Run();
        }
    }
}
