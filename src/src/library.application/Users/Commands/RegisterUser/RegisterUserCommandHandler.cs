using ErrorOr;
using library.application.Common.Interfaces.Authentication;
using library.application.Common.Interfaces.Persistance;
using library.application.Common.Interfaces.Persistance.Users;
using library.application.Users.ApplicationErrors;
using library.application.Users.Dtos;
using library.domain.Users;
using library.domain.Users.ValueObjects;
using MediatR;

namespace library.application.Users.Commands.RegisterUser;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<AuthenticationDto>>
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IPasswordProvider _passwordProvider;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IUserQueryRepository userQueryRepository,
        IUserCommandRepository userCommandRepository,
        IPasswordProvider passwordProvider,
        IJwtTokenProvider jwtTokenProvider,
        IUnitOfWork unitOfWork)
    {
        _userCommandRepository = userCommandRepository;
        _userQueryRepository = userQueryRepository;
        _passwordProvider = passwordProvider;
        _jwtTokenProvider = jwtTokenProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthenticationDto>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var errors = new List<Error>();

        if (await _userQueryRepository.CheckIfUserAlreadyIsRegisteredByEmailAsync(command.Email))
            return UsersApplicationErrors.UserIsAlreadyRegistered;

        var userIdResult = UserId.CreateIUserId(Guid.NewGuid().ToString());
        var passwordResult = Password.CreatePassword(command.Password, _passwordProvider.HashPassword);
        var emailResult = Email.CreateEmail(command.Email, isEmailConfirmed: false);
        var nameResult = Name.CreateName(command.UserName, command.FirstName, command.LastName);
        var userResult = User.CreateUser(userIdResult.Value, nameResult.Value, passwordResult.Value, emailResult.Value);

        errors.AddRange(userIdResult.ErrorsOrEmptyList);
        errors.AddRange(passwordResult.ErrorsOrEmptyList);
        errors.AddRange(emailResult.ErrorsOrEmptyList);
        errors.AddRange(nameResult.ErrorsOrEmptyList);
        errors.AddRange(userResult.ErrorsOrEmptyList);

        if (errors.Any()) return errors;

        await _userCommandRepository.AddUserAsync(userResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _jwtTokenProvider.GenerateToken(userResult.Value);

        return new AuthenticationDto(AccessToken: token, userId: userIdResult.Value.Value);
    }
}
