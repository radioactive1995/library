using ErrorOr;
using library.domain.Users.DomainErrors;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace library.domain.Users.ValueObjects;

public class Password 
{
    public string Value { get; private set; }

    public static Regex PasswordRule
        => new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&.])[A-Za-z\\d@$!%*?&.]{10,}$");

    public Password(string value)
    {
        Value = value;
    }
    public static ErrorOr<Password> CreatePassword(string value, Func<string, string> hashProvider)
    {
        if (PasswordRule.IsMatch(value) is false)
            return UsersDomainErrors.PasswordTooWeak;

        return new Password(hashProvider(value));
    }
}
