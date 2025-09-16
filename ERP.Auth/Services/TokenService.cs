using Erp.Auth.Models;
using ERP.Infrastructure.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Auth.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;

        public TokenService(JwtSettings settings)
        {
            _settings = settings;
        }

        public string GenerateToken(User user)
        {
           
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),   
                new Claim("id", user.Id.ToString()),                  
                new Claim(ClaimTypes.Name, user.Name),                
                new Claim(ClaimTypes.Email, user.Email),              
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
