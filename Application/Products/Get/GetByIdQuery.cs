using Application.Products.Responses;
using MediatR;

namespace Application.Products.Get
{
    public record GetByIdQuery(int ProductId) : IRequest<ProductResponse>;
}