using System;
using System.Linq;
using System.Linq.Expressions;
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
            await using var transaction = _context.Database.BeginTransaction();
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task<User> Get(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> IsAny(Expression<Func<User, bool>> predicate)
            => await _context.Users.AnyAsync(predicate);
    }
}
