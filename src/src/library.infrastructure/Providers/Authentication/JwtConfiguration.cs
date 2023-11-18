namespace library.infrastructure.Providers.Authentication; 
public class JwtConfiguration 
{
    public required string Secret { get; set; }
    public required int ExpiryMinutes { get; init; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
}
