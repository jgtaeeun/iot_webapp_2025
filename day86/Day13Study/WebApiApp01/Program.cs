
using Microsoft.EntityFrameworkCore;
using WebApiApp01.Models;

namespace WebApiApp01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //cors 설정
            builder.Services.AddCors(
                options =>
                {
                    options.AddPolicy("AllowFrontend", policy =>
                    {
                        policy.WithOrigins("http://localhost:5141")   //WebFrontEndApp2의 launchSettings.json의 포트번호
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
                }
            );
            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(
                options =>options.UseMySql(
                    builder.Configuration.GetConnectionString("smarthomeConnection"), 
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("smarthomeConnection"))
                    )
                );
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseCors("AllowFrontend");
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
