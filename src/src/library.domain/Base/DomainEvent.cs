using MediatR;

namespace library.domain.Base;

public abstract record DomainEvent(string Id) : INotification;
