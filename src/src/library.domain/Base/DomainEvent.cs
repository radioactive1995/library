using MediatR;

namespace library.domain.Base;

public abstract record DomainEvent : INotification
{
    public string Id { get; private set; }

    public DomainEvent()
    {
        Id = Guid.NewGuid().ToString(); 
    }
}
