using ErrorOr;
using MediatR;

namespace library.application.Books.Commands.AddBook;
public record AddBookCommand(
    string Title,
    string Author,
    string ISBN) 
    : IRequest<ErrorOr<AddBookCommandResponse>>;
