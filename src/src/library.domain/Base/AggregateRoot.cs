namespace library.domain.Base;
public abstract class AggregateRoot<TEntityId> : Entity<TEntityId>, IAggregateRoot where TEntityId : class
{
    private readonly List<DomainEvent> _domainEvents
        = new List<DomainEvent>();

    public IReadOnlyList<DomainEvent> FetchAllDomainEvents() => _domainEvents;
    public void FlushAllDomainEvents() => _domainEvents.Clear();

    public void RaiseDomainEvent(DomainEvent @event)
    {
        _domainEvents.Add(@event);
    }
}
