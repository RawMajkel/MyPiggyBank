using System;
using System.Threading.Tasks;
using MyPiggyBank.Core.Protocol.Query;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Util;

namespace MyPiggyBank.Core.Service {
    public interface IResourcesService {
        Task SaveResource(Resource source);
        Task DeleteResource(Guid resourceId);
        Task<Resource> Get(Guid id);
        PagedList<Resource> GetResources(ResourcesQuery query);    
    }
}
