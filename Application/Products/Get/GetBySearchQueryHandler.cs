using Application.Interfaces.Persistence;
using Application.Products.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Get
{
    public class GetBySearchQueryHandler : IRequestHandler<GetBySearchQuery, PaginatedResult<ProductResponse>>
    {
        private readonly IAppDbContext _context;

        public GetBySearchQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<ProductResponse>> Handle(GetBySearchQuery request, CancellationToken cancellationToken)
        {
            var productQuery = _context.Products.AsNoTracking().Where(p => p.Name.ToLower().Contains(request.SearchString)).Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                CategoryId = p.CategoryId,
                Quantity = p.Quantity,
                Price = p.Price
            }).OrderBy(p => p.Id);

            var products = await PaginatedResult<ProductResponse>.CreateAsync(productQuery, request.Page, request.PageSize);

            return products;
        }
    }
}