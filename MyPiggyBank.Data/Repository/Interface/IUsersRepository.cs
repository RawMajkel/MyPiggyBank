using System.Threading.Tasks;
using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Data.Repository {
    public interface IUsersRepository : IBaseRepository<User> {
        public Task<User> GetByEmail(string email);
    }
}
