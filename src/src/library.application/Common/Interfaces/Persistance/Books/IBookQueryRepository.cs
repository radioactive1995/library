using library.application.Books.Dtos;

namespace library.application.Common.Interfaces.Persistance.Books;

public interface IBookQueryRepository
{
    Task<bool> DoesBookExistByISBNAsync(string isbn);
    Task<BookDto?> GetBookByISBNAsync(string isbn);
}
