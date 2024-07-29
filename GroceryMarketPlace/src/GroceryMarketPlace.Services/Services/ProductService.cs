namespace GroceryMarketPlace.Services.Services
{
    using System.Linq.Expressions;
    using AutoMapper;
    using Domain.Dtos.Product;
    using Domain.Entities;
    using Domain.Enums;
    using Domain.Interfaces.Repositories;
    using Domain.Interfaces.Services;
    using Domain.Models;
    using Exceptions;
    using Microsoft.Extensions.Logging;

    public class ProductService(
        ILogger<ProductService> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService) : IProductService
    {
        public async Task<GetAllProductsResponseDto> GetAllAsync(ProductQueryParameters queryParameters)
        {
            logger.LogInformation("Get all products with query parameters {@queryParameters}", queryParameters);

            Expression<Func<Product, bool>>? filter = null;
            Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null;
            var pageNumber = queryParameters.PageNumber;
            var pageSize = queryParameters.PageSize;

            // Get product list from cache if any
            var cacheKey = $"products_{pageNumber}_{pageSize}_{queryParameters.SearchBy}_{queryParameters.SearchTerm}_{queryParameters.OrderBy}";
            var (productList, totalRecords) = await cacheService.GetDataAsync<(IEnumerable<Product> data, int totalRecords)>(cacheKey);
            IEnumerable<ProductDto> productDTOs = [];

            if (productList != null)
            {
                // Map product list to DTO
                productDTOs = mapper.Map<IEnumerable<ProductDto>>(productList);

                return new GetAllProductsResponseDto(productDTOs, totalRecords, pageNumber, pageSize);
            }

            // Search products by name
            if (queryParameters.SearchBy == "name" && queryParameters.SearchTerm != null)
            {
                filter = product => product.Name.Contains(queryParameters.SearchTerm);
            }

            // Count total of products that match the search criteria
            totalRecords = await unitOfWork.Products.CountAsync(filter: filter);

            // Get paginated product list
            productList = await unitOfWork.Products.GetAllAsync(
                filter: filter,
                orderBy: orderBy,
                pageNumber: pageNumber,
                pageSize: pageSize
            );

            // Add product list to cache
            await cacheService.SetDataAsync(cacheKey, (data: productList, totalRecords));

            // Map products to DTO
            productDTOs = mapper.Map<IEnumerable<ProductDto>>(productList);

            logger.LogInformation("Get all products with query parameters {@queryParameters} successfully", queryParameters);

            return new GetAllProductsResponseDto(productDTOs, totalRecords, pageNumber, pageSize);
        }

        public async Task<GetProductResponseDto> GetByIdAsync(string id)
        {
            logger.LogInformation("Get product with id {@id}", id);

            // Get product from cache if any
            var cacheKey = $"product_{id}";
            var product = await cacheService.GetDataAsync<Product>(cacheKey);

            if (product == null)
            {
                // Get product from DB
                product = await unitOfWork.Products.GetFirstAsync(item => item.Id == id)
                            ?? throw new NotFoundException("Product is not found", ErrorCode.ProductNotFound);

                // Cache the product
                await cacheService.SetDataAsync(cacheKey, product);
            }

            logger.LogInformation("Get product with id {@id} successfully", id);

            return mapper.Map<GetProductResponseDto>(product);
        }

        public async Task<AddProductResponseDto> AddAsync(AddProductRequestDto requestDTO)
        {
            logger.LogInformation("Add product with request {@requestDTO}", requestDTO);

            // Add product to DB
            var product = mapper.Map<Product>(requestDTO);

            unitOfWork.Products.Add(product);
            await unitOfWork.CompleteAsync();

            // Invalidate the cache for all product lists
            await cacheService.RemoveCachedDataByKeyPrefixAsync("products");

            logger.LogInformation("Add product with request {@requestDTO} successfully", requestDTO);

            return mapper.Map<AddProductResponseDto>(product);
        }

        public async Task UpdateByIdAsync(string id, UpdateProductRequestDto requestDTO)
        {
            logger.LogInformation("Update product with id {@id} and request {@requestDTO}", id, requestDTO);

            // Get product from DB
            var product = await unitOfWork.Products.GetFirstAsync(item => item.Id == id)
                            ?? throw new NotFoundException("Product is not found", ErrorCode.ProductNotFound);

            // Update product in DB
            product.Name = requestDTO.Name;
            product.Description = requestDTO.Description;
            product.StockQuantity = requestDTO.StockQuantity;
            product.Price = requestDTO.Price;

            unitOfWork.Products.Update(product);
            await unitOfWork.CompleteAsync();

            // Invalidate the cache for all product lists
            await cacheService.RemoveCachedDataByKeyPrefixAsync("products");

            logger.LogInformation("Update product with id {@id} and request {@requestDTO} successfully", id, requestDTO);
        }

        public async Task DeleteByIdAsync(string id)
        {
            logger.LogInformation("Delete product with id {@id}", id);

            // Get product from DB
            var product = await unitOfWork.Products.GetFirstAsync(item => item.Id == id)
                            ?? throw new NotFoundException("products", ErrorCode.ProductNotFound);

            // Delete product in DB
            unitOfWork.Products.Delete(product);
            await unitOfWork.CompleteAsync();

            // Invalidate the cache for all product lists
            await cacheService.RemoveCachedDataByKeyPrefixAsync("products");

            logger.LogInformation("Delete product with id {@id} successfully", id);
        }
    }
}
