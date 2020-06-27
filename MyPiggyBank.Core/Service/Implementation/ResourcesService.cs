using AutoMapper;
using MyPiggyBank.Core.Protocol.Resource;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;
using MyPiggyBank.Data.Util;
using System;
using System.Linq;
using System.Threading.Tasks;
using MyPiggyBank.Core.Protocol.Resource.Requests;
using MyPiggyBank.Core.Protocol.Resource.Responses;

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

        public PagedList<ResourceResponse> GetResources(ResourceGetRequest query)
             => PagedList<ResourceResponse>.ToPagedList(_repository.GetAll()
                .Where(r => query.UserId == Guid.Empty || r.UserId == query.UserId)
                .Where(r => r.Name == (query.Name ?? r.Name))
                .Where(r => r.Currency == (query.Currency ?? r.Currency))
                .Where(r => r.Value >= (query.MinValue ?? r.Value))
                .Where(r => r.Value <= (query.MaxValue ?? r.Value))
                .OrderBy(on => on.Name)
                .Select(r => _mapper.Map<ResourceResponse>(r)),
                query.Page, query.Limit);

        public async Task<ResourceResponse> Get(Guid id)
            => _mapper.Map<ResourceResponse>(await _repository.Get(id)) ?? throw new ArgumentException("Resource not found");

        public async Task SaveResource(ResourceSaveRequest source)
        {
            var entity = _mapper.Map<Resource>(source);
            await _repository.Add(entity);
        }

        public async Task DeleteResource(Guid resourceId)
            => await _repository.Delete(await _repository.Get(resourceId) ?? throw new ArgumentException("Resource not found"));
    }
}

