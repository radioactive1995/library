using ErrorOr;
using library.domain.Users.DomainErrors;
using System.Security.Cryptography.X509Certificates;

namespace library.domain.Users.ValueObjects;
public record UserId
{
    public string Value { get; private set; }

    private UserId(string value)
    {
        Value = value;
    }
    public static ErrorOr<UserId> CreateIUserId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return UsersDomainErrors.InvalidUserId;

        return new UserId(value);
    }
}
