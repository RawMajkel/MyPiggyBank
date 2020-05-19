using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task Add(User user);
        public Task<User> Get(Guid userId);
        public Task<bool> IsAny(Expression<Func<User, bool>> predicate);
        public Task<User> GetByEmail(string email);
    }
}
