using System;
using System.Threading.Tasks;
using MyPiggyBank.Core.Protocol;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;

namespace MyPiggyBank.Core.Service {
    public interface IAccountsService
    {
        Task SaveAccount(RegisterRequest register);
        Task DeleteAccount(Guid userId);
        Task<AuthenticateResult> Authenticate(LoginRequest loginInput);
    }

    public interface IResourcesService {
        Task SaveResource(Resource source);
        Task DeleteResource(Guid resourceId);
        Task<Resource> Get(Guid id);
        PagedList<Resource> GetResources(ResourcesQuery query);    
    }

    public interface IOperationsService {
        Task SaveOperation(Operation source);
        Task DeleteOperation(Guid resourceId);
        Task<Operation> Get(Guid id);
        PagedList<Operation> GetOperations(OperationsQuery query);
    }

    public interface IJwtService {
        LoginResponse GenerateToken(AuthenticateResult accountInfo);
    }
}
