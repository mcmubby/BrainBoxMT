using Application.Cart.Responses;
using Application.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CartQC.Get
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartResponse>
    {
        private readonly IAppDbContext _context;
        public GetCartQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<CartResponse> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var response = new CartResponse();

            var userCart = await _context.Carts
            .Where(p => p.AppUserId == request.UserId)
            .Include(ci => ci.CartItems)
            .FirstOrDefaultAsync();

            if(userCart is not null && userCart.CartItems.Any())
            {
                response.Items = userCart.CartItems.Select(ci => new ItemResponse{
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.UnitPrice * ci.Quantity,
                    Name = ci.Name
                }).ToList();

                response.TotalPrice = response.Items.Sum(ci => ci.Price);
            }

            return response;

        }
    }
}