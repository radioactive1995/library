using library.application.Users.Commands.BorrowBook;
using library.application.Users.Commands.LoginUser;
using library.application.Users.Commands.RegisterUser;
using library.contracts.Rest.Users;
using library.presentation.Endpoints.Base;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace library.presentation.Endpoints.Users;
public class UsersModule : Module, IModule
{
    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/users");

        group.MapPost("/", async (RegisterUserRequest request, ISender sender) =>
        {
            var command = (RegisterUserCommand)request;
            var result = await sender.Send(command);

            return result.Match(
                result => Results.Created(result.userId, result),
                errors => Problem(errors));
        });

        group.MapPost("/login", async (LoginUserRequest request, ISender sender) =>
        {
            var command = (LoginUserCommand)request;
            var result = await sender.Send(command);

            return result.Match(
                result => Results.Created(result.userId, result),
                errors => Problem(errors));
        });

        group.MapPatch("/book", async (BorrowBookRequest request, ISender sender, HttpContext httpContext) =>
        {
            var command = new BorrowBookCommand(httpContext.User, request.BookId);
            var result = await sender.Send(command);

            return result.Match(
                result => Results.NoContent(),
                errors => Problem(errors));
        }).RequireAuthorization();
    }
}
