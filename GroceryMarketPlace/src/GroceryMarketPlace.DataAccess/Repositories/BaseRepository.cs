namespace GroceryMarketPlace.DataAccess.Repositories
{
    using System.Linq.Expressions;
    using Data;
    using Domain.Interfaces.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext Context;
        protected readonly DbSet<T> DbSet;

        protected BaseRepository(AppDbContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            IQueryable<T> query = this.DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (pageSize != null && pageNumber != null)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetFirstAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = this.DbSet;

            query = query.Where(filter);

            if (include != null)
            {
                query = include(query);
            }

            var entity = await query.FirstOrDefaultAsync();

            return entity;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter)
        {
            IQueryable<T> query = this.DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            this.DbSet.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            this.Context.Remove(entity);
        }
    }
}
