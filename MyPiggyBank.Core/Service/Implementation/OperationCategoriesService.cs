using AutoMapper;
using MyPiggyBank.Core.Protocol.OperationCategories.Requests;
using MyPiggyBank.Core.Protocol.OperationCategories.Responses;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;
using MyPiggyBank.Data.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service.Implementation
{
    public class OperationCategoriesService : IOperationCategoriesService
    {
        private readonly IOperationCategoriesRepository _repository;
        private readonly IMapper _mapper;

        public OperationCategoriesService(IOperationCategoriesRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public PagedList<OperationCategoriesResponse> GetOperationCategories(OperationCategoriesGetRequest query, Guid userId)
             => PagedList<OperationCategoriesResponse>.ToPagedList(_repository.GetAll()
                .Where(o => o.UserId == userId)
                .Where(o => o.Name == (query.Name ?? o.Name))
                .Select(r => _mapper.Map<OperationCategoriesResponse>(r)),
                query.Page, query.Limit);

        public async Task<OperationCategoriesResponse> Get(Guid id)
            => _mapper.Map<OperationCategoriesResponse>(await _repository.Get(id)) ?? throw new ArgumentException("Operation Category not found");

        public async Task SaveOperationCategory(OperationCategoriesSaveRequest source, Guid userId)
        {
            var entity = _mapper.Map<OperationCategory>(source);
            entity.UserId = userId;
            await _repository.Add(entity);
        }
        public async Task DeleteOperationCategory(Guid operationCategoryId)
            => await _repository.Delete(await _repository.Get(operationCategoryId) ?? throw new ArgumentException("Operation Category not found"));
    }
}
