using MediatR;

namespace library.domain.Base;

public abstract record DomainEvent(Guid Id) : INotification;
