using MediatR;

namespace Application.CartQC.Create
{
    public record AddItemCommand(int UserId, int ProductId, int Quantity) : IRequest;
}