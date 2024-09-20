using AplicacionAPI_Desafio2_MM200149_PM190655.Controllers;
using AplicacionAPI_Desafio2_MM200149_PM190655.Models;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace AplicacionAPI_Desafio2_MM200149_PM190655
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ConexionContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddStackExchangeRedisOutputCache(opciones =>
            {
                opciones.Configuration = builder.Configuration.GetConnectionString("redis");
            });

            //Registrar IConnectionMultiplexer
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            builder.Services.AddOutputCache();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ConexionContext>(options => options.UseInMemoryDatabase("OrganizadoresInMemoryDB"));

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

            app.UseHttpsRedirection();

            app.UseOutputCache();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
