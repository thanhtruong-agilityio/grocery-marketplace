namespace GroceryMarketPlace.API.Controllers;

using GroceryMarketPlace.DataAccess.Data;
using GroceryMarketPlace.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ProductReviewService
{
    private readonly AppDbContext productContext;

    public ProductReviewService(AppDbContext context)
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

    public async Task<Product>? GetProductById(string productId)
    {
        return await this.productContext
            .Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task AddReview(string reviewText, List<string> photoUrls, string productId)
    {
        string userId = "matt"; // this will get changed out when we add auth

        try
        {
            // create all the photo url object
            List<ReviewPhoto> photos = new List<ReviewPhoto>();

            foreach (var photoUrl in photoUrls)
            {
                photos.Add(new ReviewPhoto { PhotoUrl = photoUrl });
            }

            // create the new review
            Review review = new()
            {
                Date = DateTime.Now,
                Photos = photos,
                Text = reviewText,
                UserId = userId
            };

            Product product = await productContext
                .Products
                .Include(p => p.Reviews)
                .FirstAsync(p => p.Id == productId);

            if (product is null)
                return;

            if (product.Reviews is null)
                product.Reviews = new List<Review>();

            product.Reviews.Add(review);

            await productContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }

    public async Task<IEnumerable<Review>> GetReviewsForProduct(string productId)
    {
        return await productContext.Reviews.AsNoTracking().Where(r => r.Product.Id == productId).ToListAsync();
    }

    public async Task<Review>? GetReviewById(int reviewId)
    {
        return await productContext
            .Reviews
            .Include(r => r.Product)
            .Include(r => r.Photos)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == reviewId);
    }
}
