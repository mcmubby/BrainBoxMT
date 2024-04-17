namespace Application.Cart.Responses
{
    public class CartResponse
    {
        public List<ItemResponse> Items { get; set; } = new List<ItemResponse>();
        public decimal TotalPrice { get; set; }
    }
}