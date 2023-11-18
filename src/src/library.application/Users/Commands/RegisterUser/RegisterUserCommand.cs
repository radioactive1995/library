using ErrorOr;
using library.application.Users.Dtos;
using MediatR;

namespace library.application.Users.Commands.RegisterUser;

public record RegisterUserCommand(
    string UserName,
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationDto>>;
