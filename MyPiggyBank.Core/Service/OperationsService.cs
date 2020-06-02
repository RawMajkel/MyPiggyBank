using System;
using System.Linq;
using System.Threading.Tasks;
using MyPiggyBank.Core.Protocol;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;

namespace MyPiggyBank.Core.Service {
    public class OperationsService : IOperationsService {
        private readonly IOperationsRepository _repository;
        public OperationsService(IOperationsRepository repository)
          => _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public PagedList<Operation> GetOperations(OperationsQuery query)
             => PagedList<Operation>.ToPagedList(_repository.GetAll()
                .Where(o => query.User == Guid.Empty || o.OperationCategory.UserId == query.User)
                .Where(o => query.Resource == Guid.Empty || o.ResourceId == query.Resource)
                .Where(o => query.OperationCategory == Guid.Empty || o.OperationCategoryId == query.OperationCategory)
                .Where(o => o.IsIncome == (query.IsIncome ?? o.IsIncome))
                .Where(o => o.Name == (query.Name ?? o.Name))
                .Where(o => o.Value >= (query.MinValue ?? o.Value))
                  .Where(o => o.Value <= (query.MaxValue ?? o.Value))
                .Where(o => o.OccuredAt >= (query.MinOccuredAt ?? o.OccuredAt))
                  .Where(o => o.OccuredAt <= (query.MinOccuredAt ?? o.OccuredAt))
                .OrderByDescending(o => o.OccuredAt),
                query.Page, query.Limit);

        public async Task<Operation> Get(Guid id)
            => await _repository.Get(id) ?? throw new ArgumentException("Operation not found");

        public async Task SaveOperation(Operation source)
            => await _repository.Add(source);
        public async Task DeleteOperation(Guid operationId)
            => await _repository.Delete(await Get(operationId));
    }
}
