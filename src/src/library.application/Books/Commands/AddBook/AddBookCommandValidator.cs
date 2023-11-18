using FluentValidation;

namespace library.application.Books.Commands.AddBook;
public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
{
    public AddBookCommandValidator()
    {
        RuleFor(e => e.Author).NotEmpty().WithErrorCode("AddBookCommand.Validation.Author").WithMessage("Author cannot be empty");
        RuleFor(e => e.Title).NotEmpty().WithErrorCode("AddBookCommand.Validation.Title").WithMessage("Title cannot be empty");
        RuleFor(e => e.ISBN).Matches(@"^\d{13}$").WithErrorCode("AddBookCommand.Validation.ISBN").WithMessage("ISBN must be exactly 13 numerical digits long");
    }
}
