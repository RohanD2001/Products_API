using Microsoft.EntityFrameworkCore;
using sampleAPI.Models;

namespace sampleAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 

        }

        public DbSet<Product> Products { get; set; }
    }
}
