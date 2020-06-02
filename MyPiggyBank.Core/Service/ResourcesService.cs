using AutoMapper;
using MyPiggyBank.Core.Protocol;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service {
    public class ResourcesService : IResourcesService
    {
        private readonly IResourcesRepository _repository;

        public ResourcesService(IResourcesRepository repository)
            => _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public PagedList<Resource> GetResources(ResourcesQuery query)
             => PagedList<Resource>.ToPagedList(_repository.GetAll()
                .Where(r => query.User == Guid.Empty || r.UserId == query.User)
                .Where(r => r.Name == (query.Name ?? r.Name))
                .Where(r => r.Currency == (query.Currency ?? r.Currency))
                .Where(r => r.Value >= (query.MinValue ?? r.Value))
                  .Where(r => r.Value <= (query.MaxValue ?? r.Value))
                .OrderBy(on => on.Name),
                query.Page, query.Limit);

        public async Task<Resource> Get(Guid id)
            => await _repository.Get(id) ?? throw new ArgumentException("Resource not found");

        public async Task SaveResource(Resource source)
            => await _repository.Add(source);

        public async Task DeleteResource(Guid resourceId)
            => await _repository.Delete(await Get(resourceId));
    }
}
