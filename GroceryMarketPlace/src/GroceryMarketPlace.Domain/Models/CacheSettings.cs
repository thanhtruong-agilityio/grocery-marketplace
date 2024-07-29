namespace GroceryMarketPlace.Domain.Models
{
    public class CacheSettings
    {
        public double SlidingExpirationInMinutes { get; set; }
        public double AbsoluteExpirationInMinutes { get; set; }
        public required Redis Redis { get; set; }
    }

    public class Redis
    {
        public required string Host { get; set; }
        public int Port { get; set; }
    }
}
