
namespace WebApiApp01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // �� ���ø����̼� ������ ���� (DI, ����, �α� �� ���� ����)
            var builder = WebApplication.CreateBuilder(args);

            // ��Ʈ�ѷ� ���񽺸� ��� (REST API ��� ���)
            builder.Services.AddControllers();

            // Swagger�� ���� ���� ��� (API ���� �ڵ� ���� ����)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ���ø����̼� ��ü ���� (HTTP ��û ���������� ���� ����)
            var app = builder.Build();

            // ���� ȯ���� ��� Swagger UI ��� ����
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();       // Swagger JSON ����
                app.UseSwaggerUI();     // Swagger UI �� �������̽� Ȱ��ȭ
            }

            // ���� ���� ���� �˻� ���� (�⺻ ���� ����)
            app.UseAuthorization();

            // ��Ʈ�ѷ� ���Ʈ ���� (��: /api/controller)
            app.MapControllers();

            // ���ø����̼� ���� ����
            app.Run();
        }
    }
}
