using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Data.Repository {

    public class OperationsRepository : BaseRepository<Operation>, IOperationsRepository
    {
        public OperationsRepository(MyPiggyBankContext context) : base(context) { }
    }
}
