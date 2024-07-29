namespace GroceryMarketPlace.Domain.Dtos.Product
{
    public class UpdateProductRequestDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int StockQuantity { get; set; }
        public double Price { get; set; }
    }
}
