namespace GroceryMarketPlace.Domain.Dtos
{
    public class BaseDto
    {
        public required string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
