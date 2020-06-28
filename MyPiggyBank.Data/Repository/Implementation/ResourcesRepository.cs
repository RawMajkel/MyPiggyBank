using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Data.Repository {
    public class ResourcesRepository : BaseRepository<Resource>, IResourcesRepository
    {
        public ResourcesRepository(MyPiggyBankContext context) : base(context) { }
    }
}
