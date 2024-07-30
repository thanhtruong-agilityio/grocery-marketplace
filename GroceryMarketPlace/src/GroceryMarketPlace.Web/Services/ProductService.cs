namespace GroceryMarketPlace.Web.Services;

using System.Net.Http.Json;
using Domain.Entities;

public class ProductService(HttpClient httpClient)
{
    public async Task<IEnumerable<Product>?> GetAllProducts() => await httpClient.GetFromJsonAsync<IEnumerable<Product>>("/products");

    public async Task<Product?>? GetProductById(int productId) => await httpClient.GetFromJsonAsync<Product>($"/products/{productId}");
}
