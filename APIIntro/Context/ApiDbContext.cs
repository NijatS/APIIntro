using APIIntro.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIIntro.Context
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
    }
}
