using MyPiggyBank.Core.Protocol.Resource.Requests;
using MyPiggyBank.Core.Protocol.Resource.Responses;
using MyPiggyBank.Data.Util;
using System;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service
{
    public interface IResourcesService
    {
        Task DeleteResource(Guid resourceId);
        Task<ResourceResponse> Get(Guid id);
        PagedList<ResourceResponse> GetResources(ResourceGetRequest query);
        Task SaveResource(ResourceSaveRequest source);
    }
}