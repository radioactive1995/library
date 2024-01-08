using library.application.Common.Interfaces.Persistance.Users;
using library.domain.Users;
using library.domain.Users.ValueObjects;
using library.infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

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
    public async Task<User?> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default)
        => await _db.Set<User>().FromSql($"SELECT * FROM dbo.Users WHERE Id = {userId}").FirstOrDefaultAsync(cancellationToken);
    public void UpdateUser(User user) 
        => _db.Set<User>().Update(user);
}
