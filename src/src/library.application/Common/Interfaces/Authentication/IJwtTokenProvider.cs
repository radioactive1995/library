using library.application.Users.Dtos;
using library.domain.Users;

namespace library.application.Common.Interfaces.Authentication;
public interface IJwtTokenProvider
{
    string GenerateToken(User User);
    string GenerateToken(UserCredentialsDto User);
}
