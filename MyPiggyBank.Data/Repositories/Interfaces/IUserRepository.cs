using System;
using System.Threading.Tasks;
using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task Add(User user);
        public Task<User> Get(Guid userId);
    }
}
