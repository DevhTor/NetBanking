
using Microsoft.EntityFrameworkCore;
using NetBanking.Api.Data;
using NetBanking.Api.Models;
using NetBanking.Api.Repositories;
using NetBanking.Api.Services;

namespace NetBanking.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Add the DbContext configuration
            builder.Services.AddDbContext<NetbankingDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register the repositories
            builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
            builder.Services.AddScoped<IRepository<Account>, AccountRepository>();
            builder.Services.AddScoped<IRepository<Transaction>, TransactionRepository>();

            // Add the specific repository registrations needed by the services
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

            // Add services for controllers
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();

            // Agrega el servicio de Swagger
            builder.Services.AddSwaggerGen();

            // Agrega los servicios de la API
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer(); // Esto es necesario para que Swagger funcione

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            // Configura los middlewares de Swagger para su uso en desarrollo
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
