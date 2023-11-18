using ErrorOr;
using library.application.Books.Dtos;
using MediatR;

namespace library.application.Books.Queries.GetBookByISBN;

public record GetBookByISBNQuery(string ISBN) : IRequest<ErrorOr<BookDto>>;
