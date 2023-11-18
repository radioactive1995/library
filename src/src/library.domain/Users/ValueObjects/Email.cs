using ErrorOr;
using library.domain.Users.DomainErrors;
using System.Text.RegularExpressions;

namespace library.domain.Users.ValueObjects;
public class Email
{
    public string Value { get; private set; }
    public bool IsEmailConfirmed { get; private set; }
    public static Regex EmailRule => new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@library\.com$");

    private Email(string value, bool isEmailConfirmed)
    {
        Value = value;
        IsEmailConfirmed = isEmailConfirmed;
    }

    public static ErrorOr<Email> CreateEmail(string value, bool isEmailConfirmed)
    {
        if (EmailRule.IsMatch(value) is false)
        {
            return UsersDomainErrors.InvalidEmailFormat;
        }

        return new Email(value, isEmailConfirmed);
    }
}
