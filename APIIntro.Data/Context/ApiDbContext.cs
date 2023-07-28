using APIIntro.Core.Entities;
using APIIntro.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace APIIntro.Data.Context
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
