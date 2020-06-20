using AutoMapper;
using MyPiggyBank.Core.Protocol.OperationCategories;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;
using MyPiggyBank.Data.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service.Implementation {
    public class OperationCategoriesService : IOperationCategoriesService
    {
        private readonly IOperationCategoriesRepository _repository;
        private readonly IMapper _mapper;

        public OperationCategoriesService(IOperationCategoriesRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public PagedList<OperationCategoryInfo> GetOperationCategories(OperationCategoriesQuery query)
             => PagedList<OperationCategoryInfo>.ToPagedList(_repository.GetAll()
                .Where(o => query.User == Guid.Empty || o.UserId == query.User)
                .Where(o => o.Name == (query.Name ?? o.Name))
                .Select(r => _mapper.Map<OperationCategoryInfo>(r)),
                query.Page, query.Limit);

        public async Task<OperationCategoryInfo> Get(Guid id)
            => _mapper.Map<OperationCategoryInfo>(await _repository.Get(id)) ?? throw new ArgumentException("Operation Category not found");

        public async Task SaveOperationCategory(OperationCategory source)
            => await _repository.Add(source);
        public async Task DeleteOperationCategory(Guid operationCategoryId)
            => await _repository.Delete(await _repository.Get(operationCategoryId) ?? throw new ArgumentException("Operation Category not found"));
    }
}
