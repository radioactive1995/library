using FluentValidation;
using System.Security.Claims;

namespace library.application.Users.Commands.BorrowBook;

public class BorrowBookCommandValidator : AbstractValidator<BorrowBookCommand>
{
    public BorrowBookCommandValidator()
    {
        RuleFor(e => e.User).NotNull().WithErrorCode("BorrowBookCommand.Validation.User").WithMessage("User cannot be null");
        RuleFor(e => e.User).Must(e => e.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier) is not null and { Value: not null or "" })
            .WithErrorCode("BorrowBookCommand.Validation.User").WithMessage("User must contain sub claim with valid value");
    }
}
