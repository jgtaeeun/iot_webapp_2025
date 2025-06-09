
namespace WebApiApp01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 웹 애플리케이션 빌더를 생성 (DI, 구성, 로깅 등 설정 포함)
            var builder = WebApplication.CreateBuilder(args);

            // 컨트롤러 서비스를 등록 (REST API 기능 사용)
            builder.Services.AddControllers();

            // Swagger를 위한 서비스 등록 (API 문서 자동 생성 도구)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 애플리케이션 객체 생성 (HTTP 요청 파이프라인 설정 가능)
            var app = builder.Build();

            // 개발 환경인 경우 Swagger UI 사용 설정
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();       // Swagger JSON 생성
                app.UseSwaggerUI();     // Swagger UI 웹 인터페이스 활성화
            }

            // 인증 이후 권한 검사 설정 (기본 설정 상태)
            app.UseAuthorization();

            // 컨트롤러 라우트 매핑 (예: /api/controller)
            app.MapControllers();

            // 애플리케이션 실행 시작
            app.Run();
        }
    }
}
