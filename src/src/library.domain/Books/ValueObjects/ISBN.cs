using ErrorOr;
using library.domain.Books.DomainErrors;
using System.Text.RegularExpressions;

namespace library.domain.Books.ValueObjects;

public record ISBN
{
    public string Value { get; private set; }
    public static Regex IsbnRule => new Regex(@"^\d{13}$");
    private ISBN(string value)
    {
        Value = value;
    }
    private ISBN() {}

    public static ErrorOr<ISBN> CreateISBN(string value)
    {
        if (IsbnRule.IsMatch(value) is false)
        {
            return BooksDomainErrors.ISBNHasToBe13DigitsLong;
        }

        return new ISBN(value);
    }
}