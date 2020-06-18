using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Data.Repository
{
    public class OperationCategoriesRepository : BaseRepository<OperationCategory>, IOperationCategoriesRepository
    {
        public OperationCategoriesRepository(MyPiggyBankContext context) : base(context) { }
    }
}
