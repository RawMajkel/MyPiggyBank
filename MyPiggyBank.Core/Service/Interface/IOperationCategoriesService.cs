using MyPiggyBank.Core.Protocol.OperationCategories.Requests;
using MyPiggyBank.Core.Protocol.OperationCategories.Responses;
using MyPiggyBank.Data.Util;
using System;
using System.Threading.Tasks;

namespace MyPiggyBank.Core.Service.Implementation
{
    public interface IOperationCategoriesService
    {
        Task DeleteOperationCategory(Guid operationCategoryId);
        Task<OperationCategoriesResponse> Get(Guid id);
        PagedList<OperationCategoriesResponse> GetOperationCategories(OperationCategoriesGetRequest query);
        Task SaveOperationCategory(OperationCategoriesSaveRequest source);
    }
}