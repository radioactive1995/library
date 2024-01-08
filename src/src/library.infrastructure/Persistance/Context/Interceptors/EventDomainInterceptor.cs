using library.domain.Base;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace library.infrastructure.Persistance.Context.Interceptors;
public class EventDomainInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;
    public EventDomainInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
    }
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            var aggregateRoots = eventData.Context.ChangeTracker
            .Entries<IAggregateRoot>().Select(entity => entity.Entity).ToList();

            List<DomainEvent> domainEvents = new List<DomainEvent>();
            
            foreach(var root in aggregateRoots)
            {
                domainEvents.AddRange(root.FetchAllDomainEvents());
                root.FlushAllDomainEvents();
            }
            
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }
        }


        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}
