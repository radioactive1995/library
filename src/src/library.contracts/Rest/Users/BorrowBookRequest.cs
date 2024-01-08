using library.application.Users.Commands.BorrowBook;
using library.application.Users.Commands.LoginUser;
using System.Security.Claims;

namespace library.contracts.Rest.Users;

public record BorrowBookRequest(string BookId);
