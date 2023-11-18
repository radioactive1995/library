using ErrorOr;
using library.domain.Books.DomainErrors;
using System.Text.RegularExpressions;

namespace library.domain.Books.ValueObjects;

public record ISBN
{
    public string Value { get; private set; }
    private static readonly Regex _isbnRule = new Regex(@"^\d{13}$");
    private ISBN(string value)
    {
        Value = value;
    }
    private ISBN() {}

    public static ErrorOr<ISBN> CreateISBN(string value)
    {
        if (_isbnRule.IsMatch(value) is false)
        {
            return BooksDomainErrors.ISBNHasToBe13DigitsLong;
        }

        return new ISBN(value);
    }
}