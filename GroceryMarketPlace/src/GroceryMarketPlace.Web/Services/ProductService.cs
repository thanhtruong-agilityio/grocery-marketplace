namespace GroceryMarketPlace.Web.Services;

using DataAccess.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ProductService
{
    private readonly AppDbContext productContext;

    public ProductService(AppDbContext context)
    {
        this.productContext = context;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await this.productContext
            .Products
            .Include(p => p.ProductType)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product> GetProductById(string productId)
    {
        return await this.productContext.Products.Where(p => p.Id == productId).AsNoTracking().FirstOrDefaultAsync();
    }
}
