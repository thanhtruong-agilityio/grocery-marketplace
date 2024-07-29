using System.Text.Json.Serialization;
using GroceryMarketPlace.API;
using GroceryMarketPlace.API.Filters;
using GroceryMarketPlace.API.Middlewares;
using GroceryMarketPlace.DataAccess;
using GroceryMarketPlace.DataAccess.Data;
using GroceryMarketPlace.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)))
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Configure API versioning
builder.Services.AddAPIVersioning();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

// Configure Fluent Validation
builder.Services.AddRequestValidation();

// Configure Data access and Business services
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DBConnection");

    options.UseSqlServer(connectionString);
});

builder.Services.AddDataAccess(builder.Configuration)
    .AddBusinessServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        // Build a Swagger endpoint for each discovered API version
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });

    // Ensure DB exists
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();

    // Seed DB
    AppDbContextSeed.RunMigrationAsync(dbContext).Wait();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }
