using MyPiggyBank.Core.Protocol.CyclicOperation;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Util;
using System;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service {
    public interface ICyclicOperationsService
    {
        Task SaveCyclicOperation(CyclicOperation source);
        Task DeleteCyclicOperation(Guid cyclicOperationId);
        Task<CyclicOperationInfo> Get(Guid id);
        PagedList<CyclicOperationInfo> GetCyclicOperations(CyclicOperationsQuery query);
    }
}