using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repositories.Base;
using MyPiggyBank.Data.Repositories.Interfaces;

namespace MyPiggyBank.Data.Repositories.Models
{
    public class UserRepository : BaseRepository, IUserRepository {
        public UserRepository(MyPiggyBankContext context) : base(context) { }

        public async Task Add(User user)
        {
            await using var transaction = _context.Database.BeginTransaction();
            await _context.AddAsync(user);
            var changedRows = await _context.SaveChangesAsync();

            if (changedRows == 0)
            {
                await transaction.RollbackAsync();
                await transaction.DisposeAsync();
                throw new DbUpdateException("Something went wrong during saving user.");
            }
            await transaction.CommitAsync();
        }

        public async Task<User> Get(Guid userId)
            => await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        public async Task<bool> IsAny(Expression<Func<User, bool>> predicate)
            => await _context.Users.AnyAsync(predicate);

        public async Task<User> GetByEmail(string email)
            => await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }
}
