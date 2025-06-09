
using Microsoft.EntityFrameworkCore;
using WebApiApp03.Models;

namespace WebApiApp03
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //db���� �ʱ�ȭ
            builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseMySql(
                  builder.Configuration.GetConnectionString("smartHomeConnection"),
                  ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("smartHomeConnection"))
              ));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
