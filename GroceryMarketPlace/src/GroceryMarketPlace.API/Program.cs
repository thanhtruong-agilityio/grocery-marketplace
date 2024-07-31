using System.Text.Json.Serialization;
using GroceryMarketPlace.API.Controllers;
using GroceryMarketPlace.DataAccess.Data;
using GroceryMarketPlace.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));


var initialScopes = builder.Configuration["AzureAdB2C:Scopes"]?.Split(' ');
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConnection = builder.Configuration["ConnectionStrings:GroceryMarketPlace:SqlDb"];

builder.Services.AddSqlServer<AppDbContext>(sqlConnection, options => options.EnableRetryOnFailure());

builder.Services.AddTransient<ProductReviewService>();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();
var scopedRequiredByApi = app.Configuration["AzureAdB2C:Scopes"] ?? "";

app.MapGet("/products", async (ProductReviewService productService) =>
    {
        return await productService.GetAllProducts();
    })
    .WithName("GetAllProducts")
    .WithOpenApi()
    .Produces<IEnumerable<Product>>(StatusCodes.Status200OK);

app.MapGet("/products/{productId}", async (ProductReviewService productService, string productId) =>
    {
        return await productService.GetProductById(productId);
    })
    .WithName("GetProductById")
    .WithOpenApi()
    .Produces<Product>(StatusCodes.Status200OK);

app.MapGet("/products/{productId}/reviews", async (ProductReviewService reviewService, string productId) =>
    {
        return await reviewService.GetReviewsForProduct(productId);
    })
    .WithName("GetReviewsForProduct")
    .WithOpenApi()
    .Produces<IEnumerable<Review>>(StatusCodes.Status200OK);

app.MapGet("/reviews/{reviewId}", async(ProductReviewService reviewService, int reviewId) =>
    {
        return await reviewService.GetReviewById(reviewId);
    })
    .WithName("GetReviewById")
    .WithOpenApi()
    .Produces<Review>(StatusCodes.Status200OK);

app.CreateDbIfNotExists();

app.Run();
