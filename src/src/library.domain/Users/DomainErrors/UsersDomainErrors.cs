using ErrorOr;

namespace library.domain.Users.DomainErrors;

public static class UsersDomainErrors
{
    public static Error NamePropertiesEmpty => Error.Failure("UsersDomainErrors.NamePropertiesEmpty", "Property values of the name cannot be empty");
    public static Error PasswordTooWeak => Error.Failure("UsersDomainErrors.PasswordTooWeak", "Password does not meet complexity requirements");
    public static Error InvalidEmailFormat => Error.Failure("UsersDomainErrors.InvalidEmailFormat", "Inserted email does not follow the format requirement");
    public static Error InvalidUserId => Error.Failure("UsersDomainErrors.InvalidUserId", "UserId value cannot be null or empty");
}
