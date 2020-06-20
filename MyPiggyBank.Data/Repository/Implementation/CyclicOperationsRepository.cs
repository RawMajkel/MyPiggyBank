using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Data.Repository
{
    public class CyclicOperationsRepository : BaseRepository<CyclicOperation>, ICyclicOperationRepository
    {
        public CyclicOperationsRepository(MyPiggyBankContext context) : base(context) { }
    }
}
