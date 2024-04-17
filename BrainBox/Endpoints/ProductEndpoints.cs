using Application.Pokemons.Exceptions;
using Application.Products;
using Application.Products.Delete;
using Application.Products.Get;
using Application.Products.Responses;
using Application.Products.Update;
using MediatR;

namespace BrainBox.Endpoints
{
    internal static class ProductEndpoints
    {
        internal static void MapProductEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("api/v1/product").WithTags("Products");


            group.MapPost("/", async (CreateProductCommand command, ISender sender) =>
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
            }).AddEndpointFilter<ValidationFilter<CreateProductCommand>>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status201Created)
            .WithOpenApi(o => new(o) { Summary = "Create a new product" });


            group.MapGet("/", async (ISender sender, string searchQuery, int page = 1, int pageSize = 20) =>
            {
                var response = await sender.Send(new GetBySearchQuery(searchQuery,page, pageSize));
                return TypedResults.Ok(response);
            }).WithOpenApi(o => new(o){ Summary = "Get a paginated list of products that match search query from the database" });


            group.MapGet("/{id:int}", async (int id, ISender sender) =>
            {
                try
                {
                    var response = await sender.Send(new GetByIdQuery(id));
                    return TypedResults.Ok(response);
                }
                catch (GenericNotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
            }).Produces(StatusCodes.Status404NotFound)
            .Produces<ProductResponse>(StatusCodes.Status200OK)
            .WithOpenApi(o => new(o) { Summary = "Get a product using its system assigned id" });


            group.MapPut("/", async (UpdateProductCommand command, ISender sender) =>
            {
                try
                {
                    await sender.Send(command);
                    return Results.Ok();
                }
                catch (GenericNotFoundException e)
                {
                    return TypedResults.BadRequest(e.Message);
                }
            }).Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(o => new(o) { Summary = "Update a product using system assigned id as key. Full payload required." });


            group.MapDelete("/{id:int}", async (int id, ISender sender) =>
            {
                try
                {
                    await sender.Send(new DeleteProductCommand(id));
                    return Results.Ok();
                }
                catch (GenericNotFoundException e)
                {
                    return TypedResults.NotFound(e.Message);
                }
            }).Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(o => new(o) { Summary = "Soft delete a product using system assigned id as key." });
        }
    }
}