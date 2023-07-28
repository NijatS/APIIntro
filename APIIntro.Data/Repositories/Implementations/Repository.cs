using APIIntro.Data.Context;
using APIIntro.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace APIIntro.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApiDbContext _context;

        public Repository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task<IQueryable<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query =_context.Set<T>().Where(expression);
            if (includes is not null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        public async Task<T> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query = _context.Set<T>().Where(expression);
            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> IsExsist(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).Count() > 0;
        }
        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
