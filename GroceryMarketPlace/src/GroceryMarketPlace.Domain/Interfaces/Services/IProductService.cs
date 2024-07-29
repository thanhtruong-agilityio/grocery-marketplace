namespace GroceryMarketPlace.Domain.Interfaces.Services
{
    using Dtos.Product;
    using Models;

    public interface IProductService
    {
        Task<GetAllProductsResponseDto> GetAllAsync(ProductQueryParameters queryParameters);
        Task<GetProductResponseDto> GetByIdAsync(string id);
        Task<AddProductResponseDto> AddAsync(AddProductRequestDto requestDto);
        Task UpdateByIdAsync(string id, UpdateProductRequestDto requestDto);
        Task DeleteByIdAsync(string id);
    }
}
