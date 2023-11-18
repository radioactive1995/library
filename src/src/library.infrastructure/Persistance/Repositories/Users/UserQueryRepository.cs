using Dapper;
using library.application.Common.Interfaces.Persistance.Users;
using library.application.Users.Dtos;
using library.domain.Users;
using library.infrastructure.Persistance.Common;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.infrastructure.Persistance.Repositories.Users;
internal class UserQueryRepository : IUserQueryRepository
{
    private readonly IOptions<DatabaseConfiguration> _options;
    public UserQueryRepository(IOptions<DatabaseConfiguration> options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<bool> CheckIfUserAlreadyIsRegisteredByEmailAsync(string email)
    {
        using IDbConnection connection = new SqlConnection(_options.Value.ConnectionString);
        return await connection.ExecuteScalarAsync<bool>(
            sql: "SELECT COUNT(1) FROM Users WHERE Email = @Email",
            param: new { Email = email },
            commandType: CommandType.Text);
    }

    public async Task<UserCredentialsDto?> GetUserCredentialsByUserNameAsync(string username)
    {
        using IDbConnection connection = new SqlConnection(_options.Value.ConnectionString);
        return await connection.QueryFirstOrDefaultAsync<UserCredentialsDto>(
            sql: "SELECT * FROM Users WHERE UserName = @UserName",
            param: new { UserName = username },
            commandType: CommandType.Text);
    }
}
