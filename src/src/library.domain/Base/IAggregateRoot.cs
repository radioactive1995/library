namespace library.domain.Base;
public interface IAggregateRoot
{
    void RaiseDomainEvent(DomainEvent @event);
    IReadOnlyList<DomainEvent> FetchAllDomainEvents();
    void FlushAllDomainEvents();
}
