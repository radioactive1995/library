using ErrorOr;
using library.application.Books.Commands.AddBook;
using library.presentation.Endpoints.Base;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using library.contracts.Rest.Books;
using Microsoft.AspNetCore.Mvc;
using library.application.Books.Queries.GetBookByISBN;

namespace library.presentation.Endpoints.Books;
public class BooksModule : Module, IModule
{
    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/books");

        group.MapGet("/{isbn}", async (ISender sender, string isbn, CancellationToken ct) =>
        {
            var query = new GetBookByISBNQuery(isbn);
            var result = await sender.Send(query, ct);

            return result.Match(
                validResponse => Results.Ok(validResponse),
                errors => Problem(errors));
        });

        group.MapPost("/", async (ISender sender, AddBookRequest request, CancellationToken ct) =>
        {
            var command = (AddBookCommand)request;
            var result = await sender.Send(command, ct);

            return result.Match(
                validResponse => Results.Created(validResponse.BookId, validResponse),
                errors => Problem(errors));
        });
    }
}
