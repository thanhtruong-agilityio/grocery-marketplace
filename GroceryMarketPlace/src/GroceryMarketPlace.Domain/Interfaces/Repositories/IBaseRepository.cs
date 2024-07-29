namespace GroceryMarketPlace.Domain.Interfaces.Repositories
{
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Query;

    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetFirstAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null
        );
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? pageNumber = null,
            int? pageSize = null
        );
        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
