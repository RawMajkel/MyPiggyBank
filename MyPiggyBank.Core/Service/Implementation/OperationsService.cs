using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPiggyBank.Core.Protocol;
using MyPiggyBank.Core.Protocol.Operation;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;
using MyPiggyBank.Data.Util;

namespace MyPiggyBank.Core.Service {
    public class OperationsService : IOperationsService
    {
        private readonly IOperationsRepository _repository;
        private readonly IMapper _mapper;

        public OperationsService(IOperationsRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public PagedList<OperationInfo> GetOperations(OperationsQuery query)
             => PagedList<OperationInfo>.ToPagedList(_repository.GetAll()
                .Where(o => query.User == Guid.Empty || o.OperationCategory.UserId == query.User)
                .Where(o => query.Resource == Guid.Empty || o.ResourceId == query.Resource)
                .Where(o => query.OperationCategory == Guid.Empty || o.OperationCategoryId == query.OperationCategory)
                .Where(o => o.IsIncome == (query.IsIncome ?? o.IsIncome))
                .Where(o => o.Name == (query.Name ?? o.Name))
                .Where(o => o.Value >= (query.MinValue ?? o.Value))
                  .Where(o => o.Value <= (query.MaxValue ?? o.Value))
                .Where(o => o.OccuredAt >= (query.MinOccuredAt ?? o.OccuredAt))
                  .Where(o => o.OccuredAt <= (query.MinOccuredAt ?? o.OccuredAt))
                .OrderByDescending(o => o.OccuredAt)
                .Select(r => _mapper.Map<OperationInfo>(r)),
                query.Page, query.Limit);

        public async Task<OperationInfo> Get(Guid id)
            => _mapper.Map<OperationInfo>(await _repository.Get(id)) ?? throw new ArgumentException("Operation not found");

        public async Task SaveOperation(Operation source)
            => await _repository.Add(source);
        public async Task DeleteOperation(Guid operationId)
            => await _repository.Delete(await _repository.Get(operationId) ?? throw new ArgumentException("Operation not found"));
    }
}
