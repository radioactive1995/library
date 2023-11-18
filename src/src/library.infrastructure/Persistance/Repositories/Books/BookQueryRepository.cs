using Dapper;
using library.application.Books.Dtos;
using library.application.Common.Interfaces.Persistance.Books;
using library.domain.Books;
using library.infrastructure.Persistance.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace library.infrastructure.Persistance.Repositories.Books;
public class BookQueryRepository : IBookQueryRepository
{
    private readonly IOptions<DatabaseConfiguration> _options;

    public BookQueryRepository(IOptions<DatabaseConfiguration> options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<bool> DoesBookExistByISBNAsync(string isbn)
    {
        using IDbConnection db = new SqlConnection(_options.Value.ConnectionString);
        return await db.ExecuteScalarAsync<bool>(
                sql: "SELECT COUNT(1) FROM Books WHERE Id = @ISBN",
                param: new { ISBN = isbn },
                commandType: CommandType.Text);
    }

    public async Task<BookDto?> GetBookByISBNAsync(string isbn)
    {
        using IDbConnection db = new SqlConnection(_options.Value.ConnectionString);
        return await db.QueryFirstOrDefaultAsync<BookDto>(
                sql: "SELECT * FROM Books WHERE Id = @ISBN",
                param: new { ISBN = isbn },
                commandType: CommandType.Text);
    }
}