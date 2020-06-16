using MyPiggyBank.Core.Protocol.Query;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository.Implementation;
using MyPiggyBank.Data.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service.Implementation
{
    public class OperationCategoriesService : IOperationCategoriesService
    {
        private readonly IOperationCategoriesRepository _repository;
        public OperationCategoriesService(IOperationCategoriesRepository repository)
          => _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public PagedList<OperationCategory> GetOperationCategories(OperationCategoriesQuery query)
             => PagedList<OperationCategory>.ToPagedList(_repository.GetAll()
                .Where(o => query.User == Guid.Empty || o.UserId == query.User)
                .Where(o => o.Name == (query.Name ?? o.Name)),
                query.Page, query.Limit);

        public async Task<OperationCategory> Get(Guid id)
            => await _repository.Get(id) ?? throw new ArgumentException("Operation Category not found");

        public async Task SaveOperationCategory(OperationCategory source)
            => await _repository.Add(source);
        public async Task DeleteOperationCategory(Guid operationCategoryId)
            => await _repository.Delete(await Get(operationCategoryId));
    }
}
