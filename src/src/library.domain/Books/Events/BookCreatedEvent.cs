using library.domain.Base;

namespace library.domain.Books.Events;
public record BookCreatedEvent : DomainEvent
{
    public BookCreatedEvent(Guid Id) : base(Id)
    {
    }
}
