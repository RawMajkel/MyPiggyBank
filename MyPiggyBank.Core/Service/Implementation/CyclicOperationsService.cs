using AutoMapper;
using MyPiggyBank.Core.Protocol.CyclicOperation;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;
using MyPiggyBank.Data.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service {
    public class CyclicOperationsService : ICyclicOperationsService
    {
        private readonly ICyclicOperationRepository _repository;
        private readonly IMapper _mapper;

        public CyclicOperationsService(ICyclicOperationRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public PagedList<CyclicOperationInfo> GetCyclicOperations(CyclicOperationsQuery query)
            => PagedList<CyclicOperationInfo>.ToPagedList(_repository.GetAll()
               .Where(o => o.OperationCategory.UserId == query.User)
               .Where(o => o.ResourceId == query.Resource)
               .Where(o => o.OperationCategoryId == query.OperationCategory)
               .Where(o => o.IsIncome == (query.IsIncome ?? o.IsIncome))
               .Where(o => o.Name == (query.Name ?? o.Name))
               .Where(o => o.EstimatedValue >= (query.MinEstimatedValue ?? o.EstimatedValue))
               .Where(o => o.EstimatedValue <= (query.MaxEstimatedValue ?? o.EstimatedValue))
               .Where(o => o.Period >= (query.MinPeriod ?? o.Period))
               .Where(o => o.Period <= (query.MaxPeriod ?? o.Period))
               .Where(o => o.NextOccurence >= (query.MinNextOccurence ?? o.NextOccurence))
               .Where(o => o.NextOccurence <= (query.MaxNextOccurence ?? o.NextOccurence))
               .OrderByDescending(o => o.NextOccurence)
               .Select(r => _mapper.Map<CyclicOperationInfo>(r)),
               query.Page, query.Limit);

        public async Task<CyclicOperationInfo> Get(Guid id)
            => _mapper.Map<CyclicOperationInfo>(await _repository.Get(id)) ?? throw new ArgumentException("Cyclic operation not found");

        public async Task SaveCyclicOperation(CyclicOperation source)
            => await _repository.Add(source);
        public async Task DeleteCyclicOperation(Guid cyclicOperationId)
            => await _repository.Delete(await _repository.Get(cyclicOperationId) ?? throw new ArgumentException("Cyclic operation not found"));
    }
}
