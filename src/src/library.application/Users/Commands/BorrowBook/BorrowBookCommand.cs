using ErrorOr;
using MediatR;
using System.Security.Claims;

namespace library.application.Users.Commands.BorrowBook;

public record BorrowBookCommand(ClaimsPrincipal User, string BookId) : IRequest<ErrorOr<Unit>>;
