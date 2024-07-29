namespace GroceryMarketPlace.DataAccess.Repositories
{
    using Data;
    using Domain.Entities;
    using Domain.Interfaces.Repositories;

    public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
    }
}
