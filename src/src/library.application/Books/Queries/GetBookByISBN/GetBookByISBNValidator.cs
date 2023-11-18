using FluentValidation;
using library.domain.Books.ValueObjects;

namespace library.application.Books.Queries.GetBookByISBN;

public class GetBookByISBNValidator : AbstractValidator<GetBookByISBNQuery>
{
    public GetBookByISBNValidator()
    {
        RuleFor(e => e.ISBN).Matches(ISBN.IsbnRule).WithErrorCode("AddBookCommand.Validation.ISBN").WithMessage("ISBN must be exactly 13 numerical digits long");
    }
}
