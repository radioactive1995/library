using ErrorOr;
using library.domain.Base;
using library.domain.Users.ValueObjects;
using System.Runtime.ExceptionServices;

namespace library.domain.Users;
public class User : AggregateRoot<UserId>
{
    public Name Name { get; private set; }
    public Password Password { get; set; }
    public Email Email { get; set; }

    private User(UserId userId, Name name, Password password, Email email)
    {
        Id = userId;
        Name = name;
        Password = password;
        Email = email;
    }

    private User() { }

    public static ErrorOr<User> CreateUser(UserId userId, Name name, Password password, Email email)
    {
        return new User(userId, name, password, email);
    }
}
