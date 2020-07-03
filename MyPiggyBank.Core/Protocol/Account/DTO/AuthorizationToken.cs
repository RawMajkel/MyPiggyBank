using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.Account.DTO
{
    public class AuthorizationToken
    {
        public string Identifier { get; set; }
        public string Token { get; set; }

        public AuthorizationToken(string identifier, string token)
        {
            Identifier = identifier;
            Token = token;
        }

        public override string ToString()
        {
            var builder = new StringBuilder()
                .AppendLine($"Identifier: {Identifier}")
                .AppendLine($"Token: {Token}");

            return builder.ToString();
        }
    }
}
