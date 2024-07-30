using GroceryMarketPlace.DataAccess.Data;
using GroceryMarketPlace.Web.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Azure;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options => options.RootDirectory = "/GroceryMarketPlace.Web/Pages");
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});


var sqlConnection = builder.Configuration["ConnectionStrings:GroceryMarketPlace:SqlDb"];
var storageConnection = builder.Configuration["ConnectionStrings:GroceryMarketPlace:Storage"];

builder.Services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Pages");
builder.Services.AddSqlServer<AppDbContext>(sqlConnection, options => options.EnableRetryOnFailure());
builder.Services.AddAzureClients(azureBuilder =>
{
    azureBuilder.AddBlobServiceClient(storageConnection);
});

builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<ReviewService>();


builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<ProductService>(client =>
{
    string productUrl = builder.Configuration["Api:ProductEndpoint"] ?? "https://localhost:7075";

    client.BaseAddress = new Uri(productUrl);
});

builder.Services.AddHttpClient<ReviewService>(client =>
{
    string reviewUrl = builder.Configuration["Api:ReviewEndpoint"] ?? "https://localhost:7075";

    client.BaseAddress = new Uri(reviewUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
