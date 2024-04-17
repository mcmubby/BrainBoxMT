using Application.Cart.Responses;
using MediatR;

namespace Application.CartQC.Get
{
    public record GetCartQuery(int UserId) : IRequest<CartResponse>;
}