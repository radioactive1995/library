using library.application.Common.Interfaces.Persistance.Books;
using library.domain.Books;
using library.infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace library.infrastructure.Persistance.Repositories.Books;

public class BookCommandRepository : IBookCommandRepository
{
    private readonly LibraryDbContext _db;
    public BookCommandRepository(LibraryDbContext db)
    {
        _db = db;
    }

    public DbSet<Book> Books { get; set; }

    public async Task AddBookAsync(Book book, CancellationToken cancellationToken = default)
        => await _db.AddAsync(book, cancellationToken);
}
