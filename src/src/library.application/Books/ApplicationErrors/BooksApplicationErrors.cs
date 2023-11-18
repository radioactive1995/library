using ErrorOr;

namespace library.application.Books.ApplicationErrors;

public static class BooksApplicationErrors
{
    public static Error BookAlreadyExists => Error.Conflict("BooksApplicationErrors.BookAlreadyExists", "Book cannot be created, it already exists");
    public static Error BookDoesNotExist => Error.NotFound("BooksApplicationErrors.BookDoesNotExist", "The book does not exist in the library system");
}
