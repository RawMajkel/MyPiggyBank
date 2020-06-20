using Microsoft.EntityFrameworkCore;
using MyPiggyBank.Data.Model;
using System.Threading.Tasks;

namespace MyPiggyBank.Data.Repository {
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(MyPiggyBankContext context) : base(context) { }

        public async Task<User> GetByEmail(string email)
            => await GetAll().FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }
}
