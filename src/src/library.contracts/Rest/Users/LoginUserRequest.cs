using library.application.Users.Commands.LoginUser;

namespace library.contracts.Rest.Users;

public record LoginUserRequest(
    string UserName,
    string Password)
{
    public static explicit operator LoginUserCommand (LoginUserRequest request)
        => new(request.UserName, request.Password);
}
