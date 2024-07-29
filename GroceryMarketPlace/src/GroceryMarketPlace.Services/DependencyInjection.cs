namespace GroceryMarketPlace.Services
{
    using GroceryMarketPlace.Domain.Interfaces.Services;
    using GroceryMarketPlace.Domain.Models;
    using GroceryMarketPlace.Services.MappingProfiles;
    using GroceryMarketPlace.Services.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using StackExchange.Redis;

    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure AutoMapper
            services.AddAutoMapper(typeof(IMappingProfilesMarker));

            // Configure settings
            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));

            // Configure Redis cache
            services.AddStackExchangeRedisCache(options => options.Configuration = configuration.GetConnectionString("RedisCache"));

            // Register services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisCache")!));

            return services;
        }
    }
}
