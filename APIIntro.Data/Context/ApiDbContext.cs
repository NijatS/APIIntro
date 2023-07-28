using APIIntro.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIIntro.Data.Context
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
    }
}
