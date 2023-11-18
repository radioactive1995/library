using ErrorOr;
using library.application.Common.Interfaces.Authentication;
using library.application.Common.Interfaces.Persistance.Users;
using library.application.Users.ApplicationErrors;
using library.application.Users.Dtos;
using MediatR;

namespace library.application.Users.Commands.LoginUser;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ErrorOr<AuthenticationDto>>
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IPasswordProvider _passwordProvider;

    public LoginUserCommandHandler(
        IUserQueryRepository userQueryRepository,
        IJwtTokenProvider jwtTokenProvider,
        IPasswordProvider passwordProvider)
    {
        _userQueryRepository = userQueryRepository;
        _jwtTokenProvider = jwtTokenProvider;
        _passwordProvider = passwordProvider;
    }

    public async Task<ErrorOr<AuthenticationDto>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _userQueryRepository.GetUserCredentialsByUserNameAsync(command.UserName);

        if (result is null)
            return UsersApplicationErrors.UserDoesNotExist;

        if (_passwordProvider.VerifyPassword(command.Password, result.Password) is false)
            return UsersApplicationErrors.WrongPassword;

        var token = _jwtTokenProvider.GenerateToken(result);

        return new AuthenticationDto(
            AccessToken: token,
            userId: result.Id);
    }
}
