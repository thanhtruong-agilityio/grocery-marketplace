namespace GroceryMarketPlace.Web.Services;

using DataAccess.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ReviewService
{
    private readonly AppDbContext pickleContext;

    public ReviewService(AppDbContext context)
    {
        this.pickleContext = context;
    }

    public async Task AddReview(string reviewText, string productId)
    {
        string userId = "matt"; // this will get changed out when we add auth

        try
        {
            // create the new review
            Review review = new()
            {
                Date = DateTime.Now,
                Text = reviewText,
                UserId = userId
            };

            Product product = await this.pickleContext.Products.FindAsync(productId);

            if (product is null)
                return;

            if (product.Reviews is null)
                product.Reviews = new List<Review>();

            product.Reviews.Add(review);

            await this.pickleContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }

    public async Task<IEnumerable<Review>> GetReviewsForProduct(string productId)
    {
        return await this.pickleContext.Reviews.AsNoTracking().Where(r => r.Product.Id == productId).ToListAsync();
    }
}
