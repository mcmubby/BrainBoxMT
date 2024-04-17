using Application.Interfaces.Persistence;
using Domain.Products;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class Initializer
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

            try
            {
                if (!dbContext.ProductCategories.Any())
                {
                    var categories = ReadCategoryCsv();
                    var products = ReadProductCsv();
                    dbContext.ProductCategories.AddRange(categories);
                    dbContext.Products.AddRange(products);
                    dbContext.AppUsers.Add(new Domain.AppUsers.AppUser { Name = "Default User"});
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while seeding data: {ex.Message}");
            }
        }

        private static IEnumerable<ProductCategory> ReadCategoryCsv()
        {
            // Skip header line when reading
            var categoryData = File.ReadLines("productCategory.csv").Skip(1);

            var categories = categoryData.Select(category =>
            {
                var data = category.Split(',');

                return new ProductCategory
                {
                    Name = data[0]
                };
            });

            return categories;
        }

        private static IEnumerable<Product> ReadProductCsv()
        {
            // Skip header line when reading
            var productData = File.ReadLines("product.csv").Skip(1);

            var products = productData.Select(product =>
            {
                var data = product.Split(',');

                return new Product
                {
                    Name = data[0],
                    CategoryId = Convert.ToInt32(data[1]),
                    Quantity = Convert.ToInt32(data[1]),
                    Price = Convert.ToInt32(data[2])
                };
            });

            return products;
        }
    }
}
