namespace GroceryMarketPlace.Web.Services;

using Domain.Entities;

public class ReviewService
{
    private readonly HttpClient reviewClient;

    public ReviewService(HttpClient client)
    {
        this.reviewClient = client;
    }

    public async Task<IEnumerable<Review>> GetReviewsForProduct(string productId)
    {
        return await this.reviewClient.GetFromJsonAsync<IEnumerable<Review>>($"/products/{productId}/reviews");
    }

    public async Task<Review>? GetReviewById(int reviewId)
    {
        return await this.reviewClient.GetFromJsonAsync<Review>($"/reviews/{reviewId}");
    }

}
