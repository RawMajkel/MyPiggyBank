using System;
using System.Threading.Tasks;
using MyPiggyBank.Core.Protocol.Query;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Util;

namespace MyPiggyBank.Core.Service {
    public interface IOperationsService {
        Task SaveOperation(Operation source);
        Task DeleteOperation(Guid resourceId);
        Task<Operation> Get(Guid id);
        PagedList<Operation> GetOperations(OperationsQuery query);
    }
}
