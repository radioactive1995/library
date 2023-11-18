using ErrorOr;
using library.application.Users.Dtos;
using MediatR;

namespace library.application.Users.Commands.LoginUser;

public record LoginUserCommand(
    string UserName,
    string Password) : IRequest<ErrorOr<AuthenticationDto>>;
