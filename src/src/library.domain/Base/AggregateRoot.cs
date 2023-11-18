namespace library.domain.Base;
public abstract class AggregateRoot<TEntityId> : Entity<TEntityId> where TEntityId : class
{
    public readonly List<DomainEvent> _domainEvents
        = new List<DomainEvent>();


    protected void RaiseDomainEvent(DomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

}
