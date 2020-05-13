using System.Threading.Tasks;

namespace MyPiggyBank.Core.Services.Account.Interface
{
    public interface IAccountService
    {
        Task SaveAccount(string userName, string password);
    }
}
