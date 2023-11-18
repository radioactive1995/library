using library.application.Common.Interfaces.Authentication;
using library.application.Users.Dtos;
using library.domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace library.infrastructure.Providers.Authentication; 


public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly JwtConfiguration _jwtConfiguration;
    public JwtTokenProvider(IOptions<JwtConfiguration> options)
    {
        _jwtConfiguration = options.Value;
    }

    public string GenerateToken(User User)
    {
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtConfiguration.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, User.Id.Value),
            new Claim(JwtRegisteredClaimNames.GivenName, User.Name.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, User.Name.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            audience: _jwtConfiguration.Audience,
            issuer: _jwtConfiguration.Issuer,
            expires: DateTime.Now.AddMinutes(_jwtConfiguration.ExpiryMinutes),
            claims: claims,
            signingCredentials:
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public string GenerateToken(UserCredentialsDto User)
    {
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtConfiguration.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, User.Id),
            new Claim(JwtRegisteredClaimNames.GivenName, User.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, User.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            audience: _jwtConfiguration.Audience,
            issuer: _jwtConfiguration.Issuer,
            expires: DateTime.Now.AddMinutes(_jwtConfiguration.ExpiryMinutes),
            claims: claims,
            signingCredentials:
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}