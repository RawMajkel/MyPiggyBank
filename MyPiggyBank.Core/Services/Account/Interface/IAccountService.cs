﻿using System.Threading.Tasks;
using MyPiggyBank.Core.Communication.Account.DTO;
using MyPiggyBank.Core.Communication.Account.Requests;

namespace MyPiggyBank.Core.Services.Account.Interface
{
    public interface IAccountService
    {
        Task SaveAccount(RegisterRequest register);
        Task<AccountInfo> Authenticate(LoginRequest loginInput);
    }
}