using library.application.Common.Interfaces.Persistance.Users;
using library.domain.Users;
using library.infrastructure.Persistance.Context;

namespace library.infrastructure.Persistance.Repositories.Users;
public class UserCommandRepository : IUserCommandRepository
{
    private readonly LibraryDbContext _db;
    public UserCommandRepository(LibraryDbContext db)
    {
        _db = db;
    }

    public async Task AddUserAsync(User user, CancellationToken cancellationToken = default)
        => await _db.AddAsync(user, cancellationToken);
}
