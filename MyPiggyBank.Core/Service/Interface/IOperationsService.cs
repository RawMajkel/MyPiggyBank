using MyPiggyBank.Core.Protocol.Operation.Requests;
using MyPiggyBank.Core.Protocol.Operation.Responses;
using MyPiggyBank.Data.Util;
using System;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service
{
    public interface IOperationsService
    {
        Task DeleteOperation(Guid operationId);
        Task<OperationResponse> Get(Guid id);
        PagedList<OperationResponse> GetOperations(OperationGetRequest response);
        Task SaveOperation(OperationSaveRequest source);
    }
}