using Microsoft.EntityFrameworkCore;
using API_wee.Models;

namespace API_wee.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<ApplicationUser> User { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Si necesitas más configuración, la añades aquí (índices, relaciones, etc).
            modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.UserName).IsUnique();
        }
    }
}
