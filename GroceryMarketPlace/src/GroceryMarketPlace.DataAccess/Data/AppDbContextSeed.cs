namespace GroceryMarketPlace.DataAccess.Data
{
    using Domain.Entities;
    using Microsoft.AspNetCore.Identity;

    public static class AppDbContextSeed
    {
        public static async Task RunMigrationAsync(AppDbContext dbContext)
        {
            if (dbContext.Products.Any())
                return;

            // Seed admin role
            var pickleType = new ProductType { Name = "Pickle" };
            var preserveType = new ProductType { Name = "Preserves" };

            var dillReview = new Review
            {
                Date = DateTime.Now,
                Text = "These pickles pack a punch",
                UserId = "matt"
            };

            var dillPickles = new Product
            {
                Description = "Deliciously dill",
                Name = "Dill Pickles",
                ProductType = pickleType,
                Reviews = new List<Review> { dillReview }
            };

            var beetReview = new Review
            {
                Date = DateTime.Now,
                Text = "Bonafide best beets",
                UserId = "matt"
            };

            var pickledBeet = new Product
            {
                Description = "unBeetable",
                Name = "Red Pickled Beets",
                StockQuantity = 10,
                Price = 20,
                ProductType = pickleType,
                Reviews = new List<Review> { beetReview }
            };

            var preserveReview = new Review
            {
                Date = DateTime.Now,
                Text = "Succulent strawberries making biscuits better",
                UserId = "matt"
            };

            var strawberryPreserves = new Product
            {
                Description = "Sweet and a treat to make your toast the most",
                Name = "Strawberry Preserves",
                StockQuantity = 10,
                Price = 25,
                ProductType = preserveType,
                Reviews = new List<Review> {  preserveReview }
            };

            dbContext.Products.Add(dillPickles);
            dbContext.Products.Add(pickledBeet);
            dbContext.Products.Add(strawberryPreserves);

            dbContext.SaveChanges();
        }
    }
}
