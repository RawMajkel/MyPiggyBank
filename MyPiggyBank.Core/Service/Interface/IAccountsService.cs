using System;
using System.Threading.Tasks;
using MyPiggyBank.Core.Protocol.Account.DTO;
using MyPiggyBank.Core.Protocol.Account.Requests;

namespace MyPiggyBank.Core.Service {
    public interface IAccountsService
    {
        Task SaveAccount(RegisterRequest register);
        Task DeleteAccount(Guid userId);
        Task<AccountInfo> Authenticate(LoginRequest loginInput);
    }
}
