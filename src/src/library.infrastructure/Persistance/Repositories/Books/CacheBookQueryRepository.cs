using library.application.Books.Dtos;
using library.application.Common.Interfaces.Persistance.Books;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;

namespace library.infrastructure.Persistance.Repositories.Books;

public class CacheBookQueryRepository : IBookQueryRepository
{
    private readonly IDistributedCache _cache;
    private readonly IBookQueryRepository _decorated;
    private readonly ILogger<CacheBookQueryRepository> _logger;

    public CacheBookQueryRepository(
        IDistributedCache cache,
        IBookQueryRepository decorated,
        ILogger<CacheBookQueryRepository> logger)
    {
        _cache = cache;
        _decorated = decorated;
        _logger = logger;
    }

    public async Task<bool> DoesBookExistByISBNAsync(string isbn)
    {
        var cacheResult = await _cache.GetAsync(isbn + nameof(DoesBookExistByISBNAsync));

        if (cacheResult is null)
        {
            _logger.LogInformation("Did not find cached book by isbn {isbn}", isbn);

            var result = await _decorated.DoesBookExistByISBNAsync(isbn);

            await _cache.SetAsync(isbn + nameof(DoesBookExistByISBNAsync), BitConverter.GetBytes(result),
                new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(1) });

            return result;
        }

        _logger.LogInformation("Found cached book by isbn {isbn}", isbn);

        return BitConverter.ToBoolean(cacheResult);
    }

    public async Task<BookDto?> GetBookByISBNAsync(string isbn)
    {
        var cacheResult = await _cache.GetAsync(isbn + nameof(GetBookByISBNAsync));

        if (cacheResult is null)
        {
            _logger.LogInformation("Did not find cached book by isbn {isbn}", isbn);

            var result = await _decorated.GetBookByISBNAsync(isbn);

            if (result is null)
                return null;

            await _cache.SetAsync(isbn + nameof(GetBookByISBNAsync), Encoding.UTF8.GetBytes(JsonSerializer.Serialize(result)), 
                new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(1)});

            return result;
        }

        _logger.LogInformation("Found cached book by isbn {isbn}", isbn);

        return JsonSerializer.Deserialize<BookDto>(Encoding.UTF8.GetString(cacheResult));
    }
}
