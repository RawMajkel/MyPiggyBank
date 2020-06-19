using System;
using System.Threading.Tasks;
using MyPiggyBank.Core.Protocol.Operation;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Util;

namespace MyPiggyBank.Core.Service {
    public interface IOperationsService {
        Task SaveOperation(Operation source);
        Task DeleteOperation(Guid resourceId);
        Task<OperationInfo> Get(Guid id);
        PagedList<OperationInfo> GetOperations(OperationsQuery query);
    }
}
