using ErrorOr;
using library.domain.Users.DomainErrors;

namespace library.domain.Users.ValueObjects;

public record Name
{
    public string UserName { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName => $"{FirstName} {LastName}";

    private Name() {}

    private Name(
        string userName,
        string firstName,
        string lastName)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
    }

    public static ErrorOr<Name> CreateName(
        string userName, 
        string firstName, 
        string lastName)
    {
        if (new[] { userName, firstName, lastName }.Any(e => string.IsNullOrWhiteSpace(e)))
        {
            return UsersDomainErrors.NamePropertiesEmpty;
        }

        return new Name(userName, firstName, lastName);
    }
}
