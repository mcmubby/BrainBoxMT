using Application.Interfaces.Persistence;
using Application.Pokemons.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IAppDbContext _context;
        public UpdateProductCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Where(p => p.Id == request.Id).FirstOrDefaultAsync() ?? throw new GenericNotFoundException("Product");

            product.Price = request.Price;
            product.Name = request.Name;
            product.CategoryId = request.CategoryId;
            product.Quantity = request.Quantity;

            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}