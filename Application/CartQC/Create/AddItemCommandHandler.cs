using Application.Interfaces.Persistence;
using Application.Pokemons.Exceptions;
using Domain.Carts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CartQC.Create
{
    public class AddItemCommandHandler : IRequestHandler<AddItemCommand>
    {
        private readonly IAppDbContext _context;

        public AddItemCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var existingCart = await _context.Carts.AsNoTracking()
                                             .Where(c => c.AppUserId == request.UserId)
                                             .Include(ci => ci.CartItems).FirstOrDefaultAsync();

            if (existingCart == null) {
                var n = new Domain.Carts.Cart
                {
                    AppUserId = request.UserId,
                };
                _context.Carts.Add(n);
                _context.SaveChanges();

                existingCart = n;
            }

            if(existingCart.CartItems.Any(p => p.ProductId == request.ProductId)) {throw new ExistingRecordException(); }

            var product = await _context.Products.Where(p => p.Id == request.ProductId).FirstOrDefaultAsync();

            var newItem = new CartItem{
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Name = product.Name,
                UnitPrice = product.Price,
                CartId = existingCart.Id
            };

            _context.CartItems.Add(newItem);
            _context.SaveChanges();
        }
    }
}