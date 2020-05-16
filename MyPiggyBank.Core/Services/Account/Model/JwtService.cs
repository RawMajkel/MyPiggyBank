using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyPiggyBank.Core.Communication.Account.DTO;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace MyPiggyBank.Core.Services.Account.Model
{
    public class JwtService
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

            var expiration = DateTime.Now.AddMinutes(20);

            var token = new JwtSecurityToken(
                signingCredentials: credentials,
                claims: claims,
                expires: expiration);

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthorizationToken(accountInfo.Email, encodeToken, expiration);
        }
    }
}
