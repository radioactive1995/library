using ErrorOr;
using library.application.Common.Interfaces.Persistance;
using library.application.Common.Interfaces.Persistance.Books;
using library.domain.Users.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace library.application.Users.Events;

public class BookBorrowedEventHandler : INotificationHandler<BookBorrowedDomainEvent>
{
    private readonly IBookCommandRepository _bookCommandRepository;
    private readonly ILogger<BookBorrowedEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public BookBorrowedEventHandler(
        IBookCommandRepository bookCommandRepository,
        ILogger<BookBorrowedEventHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _bookCommandRepository = bookCommandRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(BookBorrowedDomainEvent @event, CancellationToken cancellationToken)
    {
        var errors = new List<Error>();

        var book = await _bookCommandRepository.GetBookByIdAsync(@event.BookId);

        if (book is null)
        {
            _logger.LogError("Book with Id {BookId} does not exist in the system.", @event.BookId);
            return;
        }

        var checkoutResult = book.CheckOut(@event.UserId);
        errors.AddRange(checkoutResult.ErrorsOrEmptyList);

        if (errors.Any())
        {
            _logger.LogError("Occured error during book checkout. Error messages: {ErrorMessage}", string.Join(", ", errors.Select(e => e.Description)));
            return;
        }

        _logger.LogInformation("Successfuly checkout a book with Id: {BookId}", @event.BookId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
