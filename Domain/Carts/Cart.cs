namespace Domain.Carts
{
    public class Cart
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }

        public virtual IEnumerable<CartItem> CartItems { get; set; } 
    }
}