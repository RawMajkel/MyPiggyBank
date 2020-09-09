using Microsoft.EntityFrameworkCore;
using MyPiggyBank.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyPiggyBank.Data.Repository {
    public abstract class BaseRepository<DBO> : IBaseRepository<DBO> where DBO : BaseEntity
    {
        protected readonly MyPiggyBankContext _context;
        public BaseRepository(MyPiggyBankContext context)
        {
            _context = context;
        }

        public async Task Add(DBO dbo)
        {
            await using var transaction = _context.Database.BeginTransaction();

            var found = await Get(dbo.Id);
            if (found == null)
                await _context.AddAsync(dbo);
            else
                _context.Update(found);

            var changedRows = await _context.SaveChangesAsync();
            if (changedRows == 0) {
                await transaction.RollbackAsync();
                await transaction.DisposeAsync();
                throw new DbUpdateException("Something went wrong when adding entity.");
            }
            await transaction.CommitAsync();
        }

        public async Task Delete(DBO dbo)
        {
            await using var transaction = _context.Database.BeginTransaction();
            _context.Remove(dbo);
            var changedRows = await _context.SaveChangesAsync();

            if (changedRows == 0) {
                await transaction.RollbackAsync();
                await transaction.DisposeAsync();
                throw new DbUpdateException("Something went wrong when deleting entity.");
            }
            await transaction.CommitAsync();
        }

        public async Task DeleteBulk(IEnumerable<DBO> dbo)
        {
            await using var transaction = _context.Database.BeginTransaction();
            _context.RemoveRange(dbo);
            var changedRows = await _context.SaveChangesAsync();

            if (changedRows == 0)
            {
                await transaction.RollbackAsync();
                await transaction.DisposeAsync();
                throw new DbUpdateException("Something went wrong when deleting entity.");
            }
            await transaction.CommitAsync();
        }

        public async Task<bool> IsAny(Expression<Func<DBO, bool>> predicate)
            => await GetAll().AnyAsync(predicate);

        public async Task<DBO> Get(Guid id)
            => await GetAll().FirstOrDefaultAsync(dbo => dbo.Id == id);

        public IQueryable<DBO> GetAll()
            => _context.Set<DBO>();
    }
}
