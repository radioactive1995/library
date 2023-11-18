using ErrorOr;
using library.domain.Books.DomainErrors;

namespace library.domain.Books.ValueObjects;
public record BookStatus
{
    public NummericValue StatusValue { get; private set; }
    public string? BorrowedUserId { get; private set; }

    private BookStatus(int status)
    {
        StatusValue = (NummericValue)status;
    }

    private BookStatus(int status, string borrowedUserId)
    {
        StatusValue = (NummericValue)status;
    }

    private BookStatus() {}

    public static ErrorOr<BookStatus> CreateBookStatus(int value)
    {
        if (Enum.IsDefined(typeof(NummericValue), value))
        {
            return new BookStatus(value);
        }

        return BooksDomainErrors.BookStatusNotValid;
    }

    public static ErrorOr<BookStatus> CreateBookStatus(int value, string borrowedUserId)
    {
        if (Enum.IsDefined(typeof(NummericValue), value))
        {
            return new BookStatus(value, borrowedUserId);
        }

        return BooksDomainErrors.BookStatusNotValid;
    }

    public enum NummericValue
    {
        Available,
        Borrowed
    }
}
