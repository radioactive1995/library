using library.domain.Base;

namespace library.domain.Books.Events;
public record BookCreatedDomainEvent : DomainEvent
{
    public BookCreatedDomainEvent(string Id) : base(Id)
    {
    }
}
