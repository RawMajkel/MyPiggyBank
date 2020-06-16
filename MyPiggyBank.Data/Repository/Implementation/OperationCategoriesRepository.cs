using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Data.Repository.Implementation
{
    class OperationCategoriesRepository : BaseRepository<OperationCategory>, IOperationCategoriesRepository
    {
        public OperationCategoriesRepository(MyPiggyBankContext context) : base(context) { }
    }
}
