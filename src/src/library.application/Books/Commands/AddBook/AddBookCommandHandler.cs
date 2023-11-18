using ErrorOr;
using library.application.Books.ApplicationErrors;
using library.application.Common.Interfaces.Persistance;
using library.application.Common.Interfaces.Persistance.Books;
using library.domain.Books;
using library.domain.Books.ValueObjects;
using MediatR;

namespace library.application.Books.Commands.AddBook;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand, ErrorOr<AddBookCommandResponse>>
{
    private readonly IBookCommandRepository _bookCommandRepository;
    private readonly IBookQueryRepository _bookQueryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    public AddBookCommandHandler(
        IBookCommandRepository bookCommandRepository,
        IBookQueryRepository bookQueryRepository,
        IUnitOfWork unitOfWork)
    {
        _bookCommandRepository = bookCommandRepository;
        _bookQueryRepository = bookQueryRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<AddBookCommandResponse>> Handle(AddBookCommand command, CancellationToken cancellationToken)
    {
        List<Error> errors = new List<Error>();

        if(await _bookQueryRepository.DoesBookExistByISBNAsync(command.ISBN))
            return BooksApplicationErrors.BookAlreadyExists;

        var bookIdResult = BookId.CreateBookId(command.ISBN);
        var isbnResult = ISBN.CreateISBN(command.ISBN);
        var bookStatusResult = BookStatus.CreateBookStatus(value: 0);
        var bookResult = Book.CreateBook(
            id: bookIdResult.Value,
            command.Title,
            command.Author,
            isbnResult.Value,
            publishedDate: DateTime.Now,
            bookStatusResult.Value);

        errors.AddRange(bookIdResult.ErrorsOrEmptyList);
        errors.AddRange(isbnResult.ErrorsOrEmptyList);
        errors.AddRange(bookStatusResult.ErrorsOrEmptyList);
        errors.AddRange(bookResult.ErrorsOrEmptyList);

        if (errors.Count is not 0)
            return errors;


        await _bookCommandRepository.AddBookAsync(bookResult.Value, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AddBookCommandResponse(bookResult.Value.Id.Value); 
    }
}
