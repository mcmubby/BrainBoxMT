using Application.Interfaces.Persistence;
using Application.Pokemons.Exceptions;
using Domain.Products;
using MediatR;

namespace Application.Products
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IAppDbContext _context;
        public CreateProductCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if(_context.Products.Any(p => p.Name.ToLower() == request.Name.ToLower())){ throw new ExistingRecordException(); }

            if(!_context.ProductCategories.Any(pc => pc.Id == request.CategoryId)) {throw new GenericNotFoundException("Product Category"); }

            var product = new Product
            {
                Name = request.Name,
                CategoryId = request.CategoryId,
                Quantity = request.Quantity,
                Price = request.Price
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}