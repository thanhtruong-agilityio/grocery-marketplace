namespace GroceryMarketPlace.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        Task CompleteAsync();
    }
}
