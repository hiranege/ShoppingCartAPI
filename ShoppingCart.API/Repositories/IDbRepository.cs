
using System.Linq.Expressions;

namespace ShoppingCart.API.Repositories
{

        public interface IDbRepository<T> where T : class
        {
            Task<List<T>> GetAllAsync();
            Task<T> AddAsync(T entity);
            Task<T> UpdateAsync(T entity);
            Task<List<T>> SearcAsync(Expression<Func<T, bool>> predicate);
            Task AddRangeAsync(List<T> entities);
        

        }

}
