using MyPiggyBank.Core.Protocol.CyclicOperation.Requests;
using MyPiggyBank.Core.Protocol.CyclicOperation.Responses;
using MyPiggyBank.Data.Util;
using System;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service
{
    public interface ICyclicOperationsService
    {
        Task DeleteCyclicOperation(Guid cyclicOperationId);
        Task<CyclicOperationResponse> Get(Guid id);
        PagedList<CyclicOperationResponse> GetCyclicOperations(CyclicOperationResponse response);
        Task SaveCyclicOperation(CyclicOperationRequest source);
    }
}