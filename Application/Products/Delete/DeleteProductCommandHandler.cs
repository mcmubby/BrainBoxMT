using Application.Interfaces.Persistence;
using Application.Pokemons.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IAppDbContext _context;

        public DeleteProductCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Where(p => p.Id == request.Id).FirstOrDefaultAsync() ?? throw new GenericNotFoundException("Product");

            product.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}