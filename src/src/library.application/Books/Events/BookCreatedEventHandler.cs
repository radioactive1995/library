using library.domain.Books.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace library.application.Books.Events;
public class BookCreatedEventHandler : INotificationHandler<BookCreatedDomainEvent>
{
    private readonly ILogger<BookCreatedEventHandler> _logger;

    public BookCreatedEventHandler(
        ILogger<BookCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(BookCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Consumed: {notification}");
        return Task.CompletedTask;
    }
}
