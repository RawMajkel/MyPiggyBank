using MyPiggyBank.Core.Protocol.Query;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Util;
using System;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service
{
    public interface ICyclicOperationsService
    {
        Task DeleteCyclicOperation(Guid cyclicOperationId);
        Task<CyclicOperation> Get(Guid id);
        PagedList<CyclicOperation> GetCyclicOperations(CyclicOperationsQuery query);
        Task SaveCyclicOperation(CyclicOperation source);
    }
}