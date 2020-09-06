using AutoMapper;
using MyPiggyBank.Core.Protocol.CyclicOperation.Requests;
using MyPiggyBank.Core.Protocol.CyclicOperation.Responses;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;
using MyPiggyBank.Data.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service
{
    public class CyclicOperationsService : ICyclicOperationsService
    {
        private readonly ICyclicOperationRepository _repository;
        private readonly IMapper _mapper;

        public CyclicOperationsService(ICyclicOperationRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public PagedList<CyclicOperationResponse> GetCyclicOperations(CyclicOperationGetRequest response, Guid userId)
            => PagedList<CyclicOperationResponse>.ToPagedList(_repository.GetAll()
               .Where(o => o.OperationCategory.UserId == userId)
               .Where(o => o.ResourceId == (response.ResourceId ?? o.ResourceId))
               .Where(o => o.OperationCategoryId == (response.OperationCategoryId ?? o.OperationCategoryId))
               .Where(o => o.IsIncome == (response.IsIncome ?? o.IsIncome))
               .Where(o => o.Name == (response.Name ?? o.Name))
               .Where(o => o.EstimatedValue >= (response.MinEstimatedValue ?? o.EstimatedValue))
               .Where(o => o.EstimatedValue <= (response.MaxEstimatedValue ?? o.EstimatedValue))
               .Where(o => o.Period >= (response.MinPeriod ?? o.Period))
               .Where(o => o.Period <= (response.MaxPeriod ?? o.Period))
               .Where(o => o.NextOccurence >= (response.MinNextOccurence ?? o.NextOccurence))
               .Where(o => o.NextOccurence <= (response.MaxNextOccurence ?? o.NextOccurence))
               .OrderByDescending(o => o.NextOccurence)
               .Select(r => _mapper.Map<CyclicOperationResponse>(r)),
              response.Page, response.Limit);

        public async Task<CyclicOperationResponse> Get(Guid id)
            => _mapper.Map<CyclicOperationResponse>(await _repository.Get(id)) ?? throw new ArgumentException("Cyclic operation not found");

        public async Task SaveCyclicOperation(CyclicOperationSaveRequest source)
        {
            var entity = _mapper.Map<CyclicOperation>(source);
            await _repository.Add(entity);
        }
        public async Task DeleteCyclicOperation(Guid cyclicOperationId)
            => await _repository.Delete(await _repository.Get(cyclicOperationId) ?? throw new ArgumentException("Cyclic operation not found"));
    }
}
