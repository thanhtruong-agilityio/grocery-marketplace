// using Azure.Identity;
// using GroceryMarketPlace.Web.Services;
// using Microsoft.AspNetCore.Authentication.OpenIdConnect;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.Azure;
// using Microsoft.Identity.Web;
// using Microsoft.Identity.Web.UI;
//
// var builder = WebApplication.CreateBuilder(args);
//
// builder.Services.AddHttpContextAccessor();
//
// var initialScopes = builder.Configuration["ReviewApi:Scopes"]?.Split(' ');
//
// builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//     .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"))
//     .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
//     .AddInMemoryTokenCaches();
//
// builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();
//
// builder.Services.AddAuthorization(options =>
// {
//     options.FallbackPolicy = options.DefaultPolicy;
// });
//
// // Add services to the container.
// builder.Services.AddRazorPages();
// builder.Services.AddServerSideBlazor()
//     .AddMicrosoftIdentityConsentHandler();
//
// //var storageConnection = builder.Configuration["ConnectionStrings:Storage:DotAzure"];
//
// var storageUri = builder.Configuration["Storage:Endpoint"];
//
// builder.Services.AddAzureClients(azureBuilder =>
// {
//     azureBuilder.AddBlobServiceClient(new Uri(storageUri));
//
//     var credentials = new DefaultAzureCredential();
//
//     azureBuilder.UseCredential(credentials);
// });
//
// builder.Services.AddSingleton<ProductService>();
// builder.Services.AddTransient<ReviewService>();
//
// builder.Services.AddHttpClient<ProductService>(client =>
// {
//     string productUrl = builder.Configuration["Api:ProductEndpoint"] ?? "http://localhost:3500";
//
//     client.BaseAddress = new Uri(productUrl);
// });
//
// builder.Services.AddHttpClient<ReviewService>(client =>
// {
//     string reviewUrl = builder.Configuration["Api:ReviewEndpoint"] ?? "http://localhost:3500";
//
//     client.BaseAddress = new Uri(reviewUrl);
// });
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }
//
// app.UseHttpsRedirection();
//
// app.UseStaticFiles();
//
// app.UseRouting();
//
// app.UseAuthentication();
// app.UseAuthorization();
//
// app.MapBlazorHub();
// app.MapFallbackToPage("/_Host");
//
// app.Run();

using GroceryMarketPlace.DataAccess.Data;
using GroceryMarketPlace.Web.Services;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var sqlConnection = builder.Configuration["ConnectionStrings:SqlDb:DotAzure"];

builder.Services.AddSqlServer<AppDbContext>(sqlConnection, options => options.EnableRetryOnFailure());

builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<ReviewService>();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.CreateDbIfNotExists();

app.Run();
