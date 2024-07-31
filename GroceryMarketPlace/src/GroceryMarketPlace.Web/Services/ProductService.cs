namespace GroceryMarketPlace.Web.Services;

using System.Net.Http.Headers;
using Domain.Entities;
using Microsoft.Identity.Web;

public class ProductService
{
    private readonly HttpClient productHttp;
    private readonly IConfiguration configuration;
    private readonly ITokenAcquisition tokenAcquisition;

    public ProductService(HttpClient httpClient, ITokenAcquisition token, IConfiguration configure)
    {
        this.productHttp = httpClient;
        this.configuration = configure;
        this.tokenAcquisition = token;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        var scopes = configuration["ReviewApi:Scopes"]?.Split(' ')!;

        string accessToken = await tokenAcquisition.GetAccessTokenForUserAsync(scopes);

        this.productHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


        return await this.productHttp.GetFromJsonAsync<IEnumerable<Product>>("/products");
    }

    public async Task<Product>? GetProductById(string productId)
    {
        return await this.productHttp.GetFromJsonAsync<Product>($"/products/{productId}");
    }
}
