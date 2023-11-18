using FluentValidation;
using library.domain.Users.ValueObjects;

namespace library.application.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.Password).Matches(Password.PasswordRule).WithErrorCode("RegisterUserCommand.Validation.Password").WithMessage("Password does not meet complexity requirements");
        RuleFor(command => command.UserName).NotEmpty().WithErrorCode("RegisterUserCommand.Validation.UserName").WithMessage("Username cannot be empty");
        RuleFor(command => command.FirstName).NotEmpty().WithErrorCode("RegisterUserCommand.Validation.UserName").WithMessage("FirstName cannot be empty");
        RuleFor(command => command.LastName).NotEmpty().WithErrorCode("RegisterUserCommand.Validation.UserName").WithMessage("LastName cannot be empty");
        RuleFor(command => command.Email).Matches(Email.EmailRule).WithErrorCode("RegisterUserCommand.Validation.Email").WithMessage("Inserted email does not follow the format requirement");
    }
}
