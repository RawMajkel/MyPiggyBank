using AutoMapper;
using MyPiggyBank.Core.Protocol.Resource;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;
using MyPiggyBank.Data.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service {
    public class ResourcesService : IResourcesService
    {
        private readonly IResourcesRepository _repository;
        private readonly IMapper _mapper;

        public ResourcesService(IResourcesRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public PagedList<ResourceInfo> GetResources(ResourcesQuery query)
             => PagedList<ResourceInfo>.ToPagedList(_repository.GetAll()
                .Where(r => query.User == Guid.Empty || r.UserId == query.User)
                .Where(r => r.Name == (query.Name ?? r.Name))
                .Where(r => r.Currency == (query.Currency ?? r.Currency))
                .Where(r => r.Value >= (query.MinValue ?? r.Value))
                  .Where(r => r.Value <= (query.MaxValue ?? r.Value))
                .OrderBy(on => on.Name)
                .Select(r => _mapper.Map<ResourceInfo>(r)),
                query.Page, query.Limit);

        public async Task<ResourceInfo> Get(Guid id)
            => _mapper.Map<ResourceInfo>(await _repository.Get(id)) ?? throw new ArgumentException("Resource not found");

        public async Task SaveResource(Resource source)
            => await _repository.Add(source);

        public async Task DeleteResource(Guid resourceId)
            => await _repository.Delete(await _repository.Get(resourceId) ?? throw new ArgumentException("Resource not found"));
    }
}

