using API_wee.Models;
using Microsoft.EntityFrameworkCore;
namespace API_wee
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public ApplicationDbContext() { }

        // DbSet properties represent tables in the database
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Policy> Policy { get; set; }
        public DbSet<Client> Client { get; set; }    
    }
}
