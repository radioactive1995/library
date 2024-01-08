using ErrorOr;
using library.domain.Base;
using library.domain.Users.DomainErrors;
using library.domain.Users.Events;
using library.domain.Users.ValueObjects;

namespace library.domain.Users;
public class User : AggregateRoot<UserId>
{
    public string UserIdString => Id.Value;
    public Name Name { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }
    public List<string> BookIds { get; private set; }

    private User(UserId userId, Name name, Password password, Email email)
    {
        Id = userId;
        Name = name;
        Password = password;
        Email = email;
        BookIds = new List<string>();
    }

    private User() {}

    public ErrorOr<DomainVoid> BorrowBook(string bookId)
    {
        if (string.IsNullOrWhiteSpace(bookId))
            return UsersDomainErrors.InvalidBookId;

        if (BookIds.Count is 2)
            return UsersDomainErrors.BorrowedBooksLimit;

        if (BookIds.Any(e => e == bookId))
            return UsersDomainErrors.UserAlreadyBorrowedTheBook;

        BookIds.Add(bookId);
        RaiseDomainEvent(new BookBorrowedDomainEvent(UserId: Id.Value, BookId: bookId));

        return DomainVoid.Value;
    }

    public static ErrorOr<User> CreateUser(UserId userId, Name name, Password password, Email email)
    {
        return new User(userId, name, password, email);
    }
}
