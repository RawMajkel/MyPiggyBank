using MyPiggyBank.Core.Protocol.Query;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Util;
using System;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service.Implementation
{
    public interface IOperationCategoriesService
    {
        Task DeleteOperationCategory(Guid operationCategoryId);
        Task<OperationCategory> Get(Guid id);
        PagedList<OperationCategory> GetOperationCategories(OperationCategoriesQuery query);
        Task SaveOperationCategory(OperationCategory source);
    }
}