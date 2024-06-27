using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.Repositories;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShoppingCart.API.Repositories
{
    public class DbRepository<T> : IDbRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public DbRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


        public async Task<T> AddAsync(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> SearcAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await _dbSet.Where(predicate).ToListAsync();
           
            
            return entities;
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            _context.AddRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
