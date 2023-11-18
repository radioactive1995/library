using library.domain.Books;

namespace library.application.Common.Interfaces.Persistance.Books;

public interface IBookCommandRepository
{
    Task AddBookAsync(Book book, CancellationToken cancellationToken = default);
}
