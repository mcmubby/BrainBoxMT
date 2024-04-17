using Application.Interfaces.Persistence;
using Application.Pokemons.Exceptions;
using Application.Products.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Get
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ProductResponse>
    {
        private readonly IAppDbContext _context;
        public GetByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<ProductResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Where(p => p.Id == request.ProductId).AsNoTracking().Select(p => new ProductResponse
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Quantity = p.Quantity,
                Price = p.Price
            }).FirstOrDefaultAsync(cancellationToken);

            return product is  null ? throw new GenericNotFoundException("Product") : product;
        }
    }
}