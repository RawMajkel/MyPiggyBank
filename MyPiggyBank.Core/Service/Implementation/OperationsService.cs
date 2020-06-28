using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPiggyBank.Core.Protocol;
using MyPiggyBank.Core.Protocol.Operation;
using MyPiggyBank.Core.Protocol.Operation.Requests;
using MyPiggyBank.Core.Protocol.Operation.Responses;
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

        public PagedList<OperationResponse> GetOperations(OperationGetRequest response)
             => PagedList<OperationResponse>.ToPagedList(_repository.GetAll()
               .Where(o => o.OperationCategory.UserId == response.UserId)
               .Where(o => o.ResourceId == response.ResourceId)
               .Where(o => o.OperationCategoryId == response.OperationCategoryId)
                .Where(o => o.IsIncome == (response.IsIncome ?? o.IsIncome))
                .Where(o => o.Name == (response.Name ?? o.Name))
                .Where(o => o.Value >= (response.MinValue ?? o.Value))
                  .Where(o => o.Value <= (response.MaxValue ?? o.Value))
                .Where(o => o.OccuredAt >= (response.MinOccuredAt ?? o.OccuredAt))
                  .Where(o => o.OccuredAt <= (response.MinOccuredAt ?? o.OccuredAt))
                .OrderByDescending(o => o.OccuredAt)
                .Select(r => _mapper.Map<OperationResponse>(r)),
                response.Page, response.Limit);

        public async Task<OperationResponse> Get(Guid id)
            => _mapper.Map<OperationResponse>(await _repository.Get(id)) ?? throw new ArgumentException("Operation not found");

        public async Task SaveOperation(OperationSaveRequest source)
        {
            var entity = _mapper.Map<Operation>(source);
            await _repository.Add(entity);
        }
        public async Task DeleteOperation(Guid operationId)
            => await _repository.Delete(await _repository.Get(operationId) ?? throw new ArgumentException("Operation not found"));
    }
}
