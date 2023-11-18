using library.domain.Base;
using ErrorOr;
using library.domain.Books.DomainErrors;
using library.domain.Books.ValueObjects;
using library.domain.Books.Events;

namespace library.domain.Books;
public class Book : AggregateRoot<BookId>
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public ISBN ISBN { get; private set; }
    public DateTime PublishedDate { get; private set; }
    public BookStatus Status { get; private set; }

    private Book() {}

    private Book(BookId id, string title, string author, ISBN isbn, DateTime publishedDate, BookStatus status)
    {
        Id = id;
        Title = title;
        Author = author;
        ISBN = isbn;
        PublishedDate = publishedDate;
        Status = status;
    }

    public static ErrorOr<Book> CreateBook(BookId id, string title, string author, ISBN isbn, DateTime publishedDate, BookStatus status)
    {
        var errors = new List<Error>();
        if (new[] { title, author }.Any(e => string.IsNullOrWhiteSpace(e))
            || publishedDate == default(DateTime))
        {
            errors.Add(BooksDomainErrors.BookPropertiesEmpty);
        }

        var book = new Book(id, title, author, isbn, publishedDate, status);

        return book;
    }

    public ErrorOr<DomainVoid> CheckOut(string userId)
    {
        if (Status.StatusValue is not BookStatus.NummericValue.Available ||
            string.IsNullOrWhiteSpace(Status.BorrowedUserId) is false)
            return BooksDomainErrors.BookCannotCheckout;

        var statusResult = BookStatus.CreateBookStatus((int)BookStatus.NummericValue.Borrowed, userId);

        if (statusResult.IsError)
            return statusResult.Errors;

        Status = statusResult.Value;

        return DomainVoid.Value;
    }
}
