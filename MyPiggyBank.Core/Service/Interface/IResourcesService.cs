using System;
using System.Threading.Tasks;
using MyPiggyBank.Core.Protocol.Resource;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Util;

namespace MyPiggyBank.Core.Service {
    public interface IResourcesService {
        Task SaveResource(Resource source);
        Task DeleteResource(Guid resourceId);
        Task<ResourceInfo> Get(Guid id);
        PagedList<ResourceInfo> GetResources(ResourcesQuery query);    
    }
}
