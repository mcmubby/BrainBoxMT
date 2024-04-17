namespace Domain.Products
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}