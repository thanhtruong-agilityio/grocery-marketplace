namespace GroceryMarketPlace.Domain.Entities
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int StockQuantity { get; set; }
        public double Price { get; set; }
        public ProductType? ProductType { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
