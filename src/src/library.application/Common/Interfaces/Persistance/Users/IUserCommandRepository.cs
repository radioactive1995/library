using library.domain.Users;

namespace library.application.Common.Interfaces.Persistance.Users;
public interface IUserCommandRepository
{
    public Task AddUserAsync(User user, CancellationToken cancellationToken = default);
}
