using System;
using System.Collections.Generic;
using System.Text;
using MyPiggyBank.Core.Communication.Account.DTO;

namespace MyPiggyBank.Core.Services.Account.Interface
{
    public interface IJwtService
    {
        LoginResponse GenerateToken(AuthenticateResult accountInfo);
    }
}
