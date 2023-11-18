using FluentValidation;
using library.domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.application.Users.Commands.LoginUser;
public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(command => command.UserName).NotEmpty().WithErrorCode("LoginUserCommand.Validation.UserName").WithMessage("UserName cannot be empty");
        RuleFor(command => command.Password).Matches(Password.PasswordRule).WithErrorCode("LoginUserCommand.Validation.Password").WithMessage("Password does not meet complexity requirements");
    }
}
