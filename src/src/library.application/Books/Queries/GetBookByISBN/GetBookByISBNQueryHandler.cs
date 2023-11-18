using ErrorOr;
using library.application.Books.ApplicationErrors;
using library.application.Common.Interfaces.Persistance.Books;
using MediatR;
using library.application.Books.Dtos;

namespace library.application.Books.Queries.GetBookByISBN;

public class GetBookByISBNQueryHandler : IRequestHandler<GetBookByISBNQuery, ErrorOr<BookDto>>
{
    private readonly IBookQueryRepository _bookQueryRepository;
    public GetBookByISBNQueryHandler(IBookQueryRepository bookQueryRepository)
    {
        _bookQueryRepository = bookQueryRepository;
    }
    public async Task<ErrorOr<BookDto>> Handle(GetBookByISBNQuery query, CancellationToken cancellationToken)
    {
        var result = await _bookQueryRepository.GetBookByISBNAsync(query.ISBN);

        if (result is null)
            return BooksApplicationErrors.BookDoesNotExist;

        return result;
    }
}
