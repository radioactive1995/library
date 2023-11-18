using ErrorOr;

namespace library.application.Users.ApplicationErrors;
public static class UsersApplicationErrors
{
    public static Error UserIsAlreadyRegistered => Error.Failure("UsersApplicationErrors.UserIsAlreadyRegistered", "User is already registerd in the system");
    public static Error UserDoesNotExist => Error.Failure("UsersApplicationErrors.UserDoesNotExist", "User does not exist in the system");
    public static Error WrongPassword => Error.Failure("UsersApplicationErrors.WrongPassword", "Password is wrong for the given user");
}
