using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyPiggyBank.Core.Protocol.Account.DTO;

namespace MyPiggyBank.Core.Service
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthorizationToken GenerateToken(AccountInfo accountInfo)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authorization:SecretKey"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, accountInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, accountInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var expiration = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                issuer: _configuration["Authorization:Issuer"],
                signingCredentials: credentials,
                claims: claims,
                expires: expiration);

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthorizationToken(accountInfo.Username,accountInfo.Email, encodeToken, expiration);
        }
    }
}
