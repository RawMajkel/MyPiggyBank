using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyPiggyBank.Core.Services.Account.Interface;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repositories.Interfaces;

namespace MyPiggyBank.Core.Services.Account.Model
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher<User> _hasher;

        public AccountService(IUserRepository repository, IPasswordHasher<User> hasher)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        public Task SaveAccount(string userName, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
