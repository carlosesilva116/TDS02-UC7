using apiAutenticacao.Data;
using apiAutenticacao.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace apiAutenticacao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // registração do serviço AuthService para injeção de dependência
            // em modo scoped, ou seja, uma nova instância será criada para cada requisição HTTP
            // imbernado.
            builder.Services.AddScoped<AuthService>();



            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}