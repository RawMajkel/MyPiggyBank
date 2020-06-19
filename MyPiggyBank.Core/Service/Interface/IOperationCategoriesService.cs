using MyPiggyBank.Core.Protocol.OperationCategories;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Util;
using System;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service.Implementation {
    public interface IOperationCategoriesService
    {
        Task SaveOperationCategory(OperationCategory source);
        Task DeleteOperationCategory(Guid operationCategoryId);
        Task<OperationCategoryInfo> Get(Guid id);
        PagedList<OperationCategoryInfo> GetOperationCategories(OperationCategoriesQuery query);
    }
}