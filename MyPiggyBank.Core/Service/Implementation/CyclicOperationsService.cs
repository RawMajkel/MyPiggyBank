using MyPiggyBank.Core.Protocol.Query;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;
using MyPiggyBank.Data.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service
{
    public class CyclicOperationsService
    {
        private readonly ICyclicOperationRepository _repository;
        public CyclicOperationsService(ICyclicOperationRepository repository)
          => _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        public PagedList<CyclicOperation> GetCyclicOperations(CyclicOperationsQuery query)
            => PagedList<CyclicOperation>.ToPagedList(_repository.GetAll()
               .Where(o => query.User == Guid.Empty || o.OperationCategory.UserId == query.User)
               .Where(o => query.Resource == Guid.Empty || o.ResourceId == query.Resource)
               .Where(o => query.OperationCategory == Guid.Empty || o.OperationCategoryId == query.OperationCategory)
               .Where(o => o.IsIncome == (query.IsIncome ?? o.IsIncome))
               .Where(o => o.Name == (query.Name ?? o.Name))
               .Where(o => o.EstimatedValue >= (query.MinEstimatedValue ?? o.EstimatedValue))
               .Where(o => o.EstimatedValue <= (query.MaxEstimatedValue ?? o.EstimatedValue))
               .Where(o => o.Period >= (query.MinPeriod ?? o.Period))
               .Where(o => o.Period <= (query.MaxPeriod ?? o.Period))
               .Where(o => o.NextOccurence >= (query.MinNextOccurence ?? o.NextOccurence))
               .Where(o => o.NextOccurence <= (query.MaxNextOccurence ?? o.NextOccurence))
               .OrderByDescending(o => o.NextOccurence),
               query.Page, query.Limit);

        public async Task<CyclicOperation> Get(Guid id)
            => await _repository.Get(id) ?? throw new ArgumentException("Cyclic operation not found");

        public async Task SaveCyclicOperation(CyclicOperation source)
            => await _repository.Add(source);
        public async Task DeleteCyclicOperation(Guid cyclicOperationId)
            => await _repository.Delete(await Get(cyclicOperationId));
    }
}
