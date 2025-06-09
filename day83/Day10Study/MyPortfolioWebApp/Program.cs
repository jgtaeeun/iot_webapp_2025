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
            // 웹 애플리케이션 빌더 생성
            var builder = WebApplication.CreateBuilder(args);

            // MVC 패턴을 위한 컨트롤러 + 뷰 서비스 등록
            builder.Services.AddControllersWithViews();

            // 데이터베이스 연결 (MySQL)
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("smartHomeConnection"), // 연결 문자열
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("smartHomeConnection")) // MySQL 서버 버전 자동 감지
                ));

            // ASP.NET Core Identity 설정 (사용자 인증/권한 관리)
            builder.Services.AddIdentity<CustomUser, IdentityRole>()   // 사용자 클래스와 역할(Role) 지정
                .AddEntityFrameworkStores<ApplicationDBContext>()      // 사용자 정보를 EF Core로 저장
                .AddDefaultTokenProviders();                           // 이메일 인증 등 토큰 관련 기능

            // 비밀번호 정책 설정 (보안 요구사항 완화)
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 4;             // 최소 길이: 4자
                options.Password.RequireNonAlphanumeric = false; // 특수문자 불필요
                options.Password.RequireUppercase = false;       // 대문자 불필요
                options.Password.RequireLowercase = false;       // 소문자 불필요
                options.Password.RequireDigit = false;           // 숫자 불필요
            });

            // 애플리케이션 객체 생성
            var app = builder.Build();

            // 개발 환경이 아닌 경우 예외 처리 페이지 설정
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error"); // 사용자 정의 에러 페이지로 리디렉션
            }

            app.UseStaticFiles();   // 정적 파일(css, js 등) 제공

            app.UseRouting();       // 라우팅 설정 활성화

            app.UseAuthorization(); // 권한 검사 미들웨어

            app.UseAuthentication(); // 사용자 인증 미들웨어 (순서 중요: 인증 -> 권한 검사)

            // 기본 라우팅 설정 (기본값: HomeController의 Index 액션)
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // 애플리케이션 실행
            app.Run();
        }
    }
}
