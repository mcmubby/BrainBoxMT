using Application.Products.Responses;
using MediatR;

namespace Application.Products.Get
{
    public record GetBySearchQuery(string SearchString, int Page, int PageSize) : IRequest<PaginatedResult<ProductResponse>>;
}