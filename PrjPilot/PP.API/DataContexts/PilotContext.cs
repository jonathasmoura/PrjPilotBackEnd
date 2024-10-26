using Microsoft.EntityFrameworkCore;
using PP.API.Models;

namespace PP.API.DataContexts
{
    public class PilotContext : DbContext
    {
        public PilotContext(DbContextOptions<PilotContext> options)
        : base(options) { }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
