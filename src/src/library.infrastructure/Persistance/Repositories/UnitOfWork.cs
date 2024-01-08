using library.application.Common.Interfaces.Persistance;
using library.infrastructure.Persistance.Context;

namespace library.infrastructure.Persistance.Repositories;
public class UnitOfWork : IUnitOfWork
{
    public readonly LibraryDbContext _db;

    public UnitOfWork(LibraryDbContext db)
    {
        _db = db;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct) => await _db.SaveChangesAsync(ct);
}
