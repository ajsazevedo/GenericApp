using GenericApp.Domain.Dto.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GenericApp.Domain.Interfaces.Services
{
    public interface ITokenManagerService
    {
        JwtSecurityTokenHandler TokenHandler { get; }
        ClaimsIdentity CreateIdentity(UserDto user);
        string GenerateToken(UserDto user);
        string GenerateToken(ClaimsIdentity identity);
        DateTime GetExpireDate(string token);
    }
}
