namespace library.application.Common.Interfaces.Authentication;
public interface IPasswordProvider
{
    public string HashPassword(string value);
    public bool VerifyPassword(string inputPassword, string hashedPassword);
}
