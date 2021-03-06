﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.Account.DTO
{
    public class AuthorizationToken
    {
        public string Username { get; set; }
        public string Identifier { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public AuthorizationToken(string username, string identifier, string token, DateTime expiration)
        {
            Username = username;
            Identifier = identifier;
            Token = token;
            Expiration = expiration;
        }

        public override string ToString()
        {
            var builder = new StringBuilder()
                .AppendLine($"Username: {Username}")
                .AppendLine($"Identifier: {Identifier}")
                .AppendLine($"Token: {Token}")
                .AppendLine($"Expires at: {Expiration.ToString("dd MMM yyyy HH:mm:ss")}");

            return builder.ToString();
        }
    }
}
