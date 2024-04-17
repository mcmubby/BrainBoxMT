using MediatR;

namespace Application.Products.Update
{
    public class UpdateProductCommand : IRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}