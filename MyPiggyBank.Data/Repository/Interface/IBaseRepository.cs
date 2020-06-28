using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Data.Repository {
    public interface IBaseRepository<DBO> where DBO : BaseEntity {
        public Task Add(DBO dbo);
        public Task Delete(DBO dbo);

        public IQueryable<DBO> GetAll();
        public Task<DBO> Get(Guid id);

        public Task<bool> IsAny(Expression<Func<DBO, bool>> predicate);
    }
}
