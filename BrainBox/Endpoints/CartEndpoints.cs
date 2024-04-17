using Application.Cart.Responses;
using Application.CartQC.Create;
using Application.CartQC.Get;
using Application.Pokemons.Exceptions;
using MediatR;

namespace BrainBox.Endpoints
{
    internal static class CartEndpoints
    {
        internal static void MapCartEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("api/v1/cart").WithTags("Carts");

            group.MapPost("/", async (AddItemCommand command, ISender sender) =>
            {
                try
                {
                    await sender.Send(command);
                    return Results.Created();
                }
                catch (ExistingRecordException e)
                {
                    return Results.BadRequest(e.Message);
                }
            }).Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status201Created)
            .WithOpenApi(o => new(o) { Summary = "Add item to cart" });


            group.MapGet("/{id:int}", async (int id, ISender sender) =>
            {
                try
                {
                    var response = await sender.Send(new GetCartQuery(id));
                    return TypedResults.Ok(response);
                }
                catch (GenericNotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
            }).Produces(StatusCodes.Status404NotFound)
            .Produces<CartResponse>(StatusCodes.Status200OK)
            .WithOpenApi(o => new(o) { Summary = "Get cart using its user id" });
            
        }
    }
}