using library.application.Books.Commands.AddBook;

namespace library.contracts.Rest.Books;

public record AddBookRequest(
    string Title, 
    string Author, 
    string ISBN)
{
    public static explicit operator AddBookCommand(AddBookRequest request)
        => new(request.Title, request.Author, request.ISBN);
}