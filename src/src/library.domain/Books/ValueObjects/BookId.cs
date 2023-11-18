using ErrorOr;
using library.domain.Books.DomainErrors;

namespace library.domain.Books.ValueObjects;
public record BookId
{
    public string Value { get; private set; }

    private BookId(string value)
    {
        Value = value;
    }

    public static ErrorOr<BookId> CreateBookId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return BooksDomainErrors.InvalidBookId;

        return new BookId(value);
    }
}
