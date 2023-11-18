using ErrorOr;

namespace library.domain.Books.DomainErrors;

public static class BooksDomainErrors
{
    public static Error ISBNHasToBe13DigitsLong => Error.Failure("BooksDomainErrors.ISBNHasToBe13DigitsLong", "ISBN has to be 13 digits long value");
    public static Error BookPropertiesEmpty => Error.Failure("BooksDomainErrors.BookPropertiesEmpty", "Property values of the book cannot be empty");
    public static Error BookStatusNotValid => Error.Failure("BooksDomainErrors.BookStatusNotValid", "Book status value is not valid, out of range");
    public static Error BookCannotReturn => Error.Conflict("BooksDomainErrors.BookCannotReturn", "The book is not available at this moment");
    public static Error BookCannotCheckout => Error.Conflict("BooksDomainErrors.BookCannotCheckout", "The book is already available, cannot checkout");
    public static Error InvalidBookId => Error.Failure("BooksDomainErrors.InvalidBookId", "The bookId value cannot be null or empty");
}
