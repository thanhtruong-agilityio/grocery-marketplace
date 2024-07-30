namespace GroceryMarketPlace.Web.Services;

using System.Net.Http.Json;
using GroceryMarketPlace.Domain.Entities;
using Microsoft.Identity.Web;

public class ReviewService
{
	private readonly HttpClient _reviewClient;
	private readonly ITokenAcquisition _tokenAcquisition;
	private readonly IConfiguration _configuration;

    public ReviewService(HttpClient client, ITokenAcquisition token, IConfiguration configure)
	{
		this._reviewClient = client;
		this._tokenAcquisition = token;
		this._configuration = configure;
	}

	public async Task AddReview(string reviewText, List<string> photoUrls, int productId)
	{
		try
		{
			// NewReview newReview = new NewReview
			// {
			// 	PhotoUrls = photoUrls,
			// 	ProductId = productId,
			// 	ReviewText = reviewText
			// };
   //
   //          var scopes = this.configuration["ReviewApi:Scopes"]?.Split(' ')!;
   //
   //          string accessToken = await this.tokenAcquisition.GetAccessTokenForUserAsync(scopes);
   //
			// this.reviewClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
   //
			// await this.reviewClient.PostAsJsonAsync<NewReview>("/reviews", newReview);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
    }

	public async Task<IEnumerable<Review>> GetReviewsForProduct(int productId)
	{
		return await this._reviewClient.GetFromJsonAsync<IEnumerable<Review>>($"/products/{productId}/reviews");
	}

	public async Task<Review>? GetReviewById(int reviewId)
	{
		return await this._reviewClient.GetFromJsonAsync<Review>($"/reviews/{reviewId}");
	}

}
