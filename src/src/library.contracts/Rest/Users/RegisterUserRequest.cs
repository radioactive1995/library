using library.application.Users.Commands.RegisterUser;

namespace library.contracts.Rest.Users;

public record RegisterUserRequest(string UserName,
    string FirstName,
    string LastName,
    string Email,
    string Password)
{

    public static explicit operator RegisterUserCommand(RegisterUserRequest request)
        => new(request.UserName, request.FirstName, request.LastName, request.Email, request.Password);
}
