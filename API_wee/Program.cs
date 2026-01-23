using Microsoft.EntityFrameworkCore;
namespace API_wee
{
    public class Program
    {
        public static void Main(string[] args)
        {
          

            var builder = WebApplication.CreateBuilder(args);
    
            // Add services to the container.

            builder.Services.AddControllers();

            // Add the DbContext service to the dependency injection container
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)); // Use the appropriate provider (UseSqlite, UseNpgsql, etc.)


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
