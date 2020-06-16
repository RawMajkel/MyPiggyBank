using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Data.Repository.Implementation
{
    class OperationsCategoryRepository : BaseRepository<OperationCategory>, IOperationsCategoryRepository
    {
        public OperationsCategoryRepository(MyPiggyBankContext context) : base(context) { }
    }
}
