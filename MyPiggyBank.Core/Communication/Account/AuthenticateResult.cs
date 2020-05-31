using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Communication.Account.DTO
{
    public class AuthenticateResult
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
