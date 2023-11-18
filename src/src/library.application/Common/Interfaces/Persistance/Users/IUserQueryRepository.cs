using library.application.Users.Dtos;
using library.domain.Users;

namespace library.application.Common.Interfaces.Persistance.Users;
public interface IUserQueryRepository
{
    Task<bool> CheckIfUserAlreadyIsRegisteredByEmailAsync(string email);
    Task<UserCredentialsDto?> GetUserCredentialsByUserNameAsync(string username);
}
