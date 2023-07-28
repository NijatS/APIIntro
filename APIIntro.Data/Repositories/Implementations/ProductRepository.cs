using APIIntro.Data.Context;
using APIIntro.Core.Entities;
using APIIntro.Core.Repositories.Interfaces;
using APIIntro.Repositories.Implementations;

namespace APIIntro.Data.Repositories.Implementations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
