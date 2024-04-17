using Domain.Products;

namespace Domain.Carts
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int CartId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}