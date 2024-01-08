using library.domain.Base;

namespace library.domain.Users.Events;

public record BookBorrowedDomainEvent(string UserId, string BookId) : DomainEvent();
