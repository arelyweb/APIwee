using API_wee.Data;
using API_wee.Repositories;
using API_wee.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
using System.Text;
namespace API_wee
{
    public class Program
    {
        public static void Main(string[] args)
        {
          

            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            var jwtSection = configuration.GetSection("Jwt");
            var key = jwtSection.GetValue<string>("Key");
            var issuer = jwtSection.GetValue<string>("Issuer");
            var audience = jwtSection.GetValue<string>("Audience");

            // Add services to the container.

            builder.Services.AddControllers();

            // Add the DbContext service to the dependency injection container
            var conn = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(conn)); 

            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddSingleton<ITokenService, TokenService>();
            builder.Services.AddSingleton<IRefreshTokenRepository, InMemoryRefreshTokenRepository>();

            // Add authentication
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(30)
                    };
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // la URL de tu Angular
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            // Authorization: puedes añadir políticas si quieres
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers();

            var app = builder.Build();
            app.UseCors("AllowFrontend");
            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
