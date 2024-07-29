namespace GroceryMarketPlace.Domain.Models
{
    public class BaseQueryParameters
    {
        public string? SearchBy { get; set; }
        public string? SearchTerm { get; set; }
        public string? OrderBy { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
