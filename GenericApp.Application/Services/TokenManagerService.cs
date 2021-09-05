using GenericApp.Domain.Dto.Models;
using GenericApp.Domain.Interfaces.Services;
using GenericApp.Infra.CC;
using GenericApp.Infra.CC.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GenericApp.Application.Services
{
    public class TokenManagerService : ITokenManagerService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly TokenConfigurations _tokenConfigurations;

        public TokenManagerService()
        {
            _tokenHandler = new JwtSecurityTokenHandler();
            _tokenConfigurations = new TokenConfigurations(Global.Instance.GetTokenConfiguration());
        }

        public JwtSecurityTokenHandler TokenHandler => _tokenHandler;

        public ClaimsIdentity CreateIdentity(UserDto user)
        {
            return new ClaimsIdentity(
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(ClaimTypes.Email, user.Email ),
                        new Claim(ClaimTypes.GivenName, user.Name ),
                        new Claim(ClaimTypes.Sid, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                    }
                );
        }

        public string GenerateToken(UserDto user)
        {
            var identity = CreateIdentity(user);
            return GenerateToken(identity);
        }

        public string GenerateToken(ClaimsIdentity identity)
        {
            var creationDate = DateTime.Now;
            var expirationDate = creationDate.AddSeconds(_tokenConfigurations.Seconds);

            var securityToken = _tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _tokenConfigurations.SigningCredentials,
                Subject = identity,
                Expires = expirationDate,
                NotBefore = creationDate
            });

            return _tokenHandler.WriteToken(securityToken);
        }

        public DateTime GetExpireDate(string token)
        {
            var jwt = _tokenHandler.ReadJwtToken(token);
            return jwt.ValidTo;
        }
    }
}
