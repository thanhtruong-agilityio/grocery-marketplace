namespace GroceryMarketPlace.Domain.Dtos.Product
{
    public class GetAllProductsResponseDto(
        IEnumerable<ProductDto> data,
        int totalRecords,
        int? pageNumber,
        int? pageSize) : BasePaginationResponseDto<ProductDto>(data, totalRecords, pageNumber, pageSize)
    {
    }
}
