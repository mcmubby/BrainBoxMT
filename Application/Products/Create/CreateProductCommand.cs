using MediatR;

namespace Application.Products
{
    public record CreateProductCommand(string Name, int CategoryId, int Quantity, decimal Price) : IRequest;
}