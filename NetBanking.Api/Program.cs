using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetBanking.Api.Data;
using NetBanking.Api.Models;
using NetBanking.Api.Repositories;
using NetBanking.Api.Services;
using System.Text;

namespace NetBanking.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            // Add the DbContext configuration
            builder.Services.AddDbContext<NetbankingDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register the repositories
            builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
            builder.Services.AddScoped<IRepository<Account>, AccountRepository>();
            builder.Services.AddScoped<IRepository<Transaction>, TransactionRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

            // Add services for controllers
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();

            // NUEVO: Registra los servicios de autenticación
            builder.Services.AddScoped<IAuthService, AuthService>();

            // NUEVO: Configuración de la autenticación JWT
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Solo para desarrollo
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Agrega el servicio de Swagger
            builder.Services.AddSwaggerGen();

            // Agrega los servicios de la API
            builder.Services.AddEndpointsApiExplorer();

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            // Agrega el servicio de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // NUEVO: Habilita los middlewares de autenticación y autorización

            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}