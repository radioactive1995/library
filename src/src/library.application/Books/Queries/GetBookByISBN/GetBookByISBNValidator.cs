using FluentValidation;

namespace library.application.Books.Queries.GetBookByISBN;

public class GetBookByISBNValidator : AbstractValidator<GetBookByISBNQuery>
{
    public GetBookByISBNValidator()
    {
        RuleFor(e => e.ISBN).Matches(@"^\d{13}$").WithErrorCode("AddBookCommand.Validation.ISBN").WithMessage("ISBN must be exactly 13 numerical digits long");
    }
}
