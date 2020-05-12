using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repositories.Base;
using MyPiggyBank.Data.Repositories.Interfaces;

namespace MyPiggyBank.Data.Repositories.Models
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(MyPiggyBankContext context) : base(context)
        {
        }

        public async Task Add(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Get(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
