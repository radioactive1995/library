namespace library.application.Users.Dtos;
public record UserCredentialsDto(
    string Id,
    string FirstName,
    string LastName,
    string Password)
{
    public UserCredentialsDto() : this(string.Empty, string.Empty, string.Empty, string.Empty) {}
}
