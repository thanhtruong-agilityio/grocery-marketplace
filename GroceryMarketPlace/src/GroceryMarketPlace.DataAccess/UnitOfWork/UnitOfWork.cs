namespace GroceryMarketPlace.DataAccess.UnitOfWork
{
    using Data;
    using Domain.Interfaces.Repositories;

    public class UnitOfWork(
        AppDbContext dbContext,
        IProductRepository productRepository) : IUnitOfWork
    {
        public IProductRepository Products { get; } = productRepository;

        public async Task CompleteAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }
    }
}
