using Microsoft.EntityFrameworkCore;
using API_wee.Models;

namespace API_wee.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<ApplicationUser> User { get; set; } = null!;
        public DbSet<Policy> Policy { get; set; } = null!;
        public DbSet<Client> Client { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Si necesitas más configuración, la añades aquí (índices, relaciones, etc).
            modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<Policy>().HasIndex(u => u.Id_policy).IsUnique();
            modelBuilder.Entity<Client>().HasIndex(u => u.IdClient).IsUnique();
        }
    }
}
