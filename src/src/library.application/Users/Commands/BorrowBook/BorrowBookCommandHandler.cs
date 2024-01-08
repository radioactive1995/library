using ErrorOr;
using library.application.Common.Interfaces.Persistance;
using library.application.Common.Interfaces.Persistance.Users;
using library.application.Users.ApplicationErrors;
using MediatR;
using System.Security.Claims;

namespace library.application.Users.Commands.BorrowBook;
public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, ErrorOr<Unit>>
{
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BorrowBookCommandHandler(
        IUserCommandRepository userCommandRepository,
        IUnitOfWork unitOfWork)
    {
        _userCommandRepository = userCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Unit>> Handle(BorrowBookCommand command, CancellationToken cancellationToken)
    {
        var errors = new List<Error>();

        var userId = command.User.Claims.First(e => e.Type == ClaimTypes.NameIdentifier).Value;

        var user = await _userCommandRepository.GetUserByIdAsync(userId);

        if (user == null)
            return UsersApplicationErrors.UserDoesNotExist;

        var borrowBookResult = user.BorrowBook(command.BookId);

        errors.AddRange(borrowBookResult.ErrorsOrEmptyList);

        if (errors.Count > 0)
            return errors;

        _userCommandRepository.UpdateUser(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
