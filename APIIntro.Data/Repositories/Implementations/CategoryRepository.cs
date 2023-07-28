using APIIntro.Data.Context;
using APIIntro.Core.Entities;
using APIIntro.Core.Repositories.Interfaces;
using APIIntro.Repositories.Implementations;

namespace APIIntro.Data.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
