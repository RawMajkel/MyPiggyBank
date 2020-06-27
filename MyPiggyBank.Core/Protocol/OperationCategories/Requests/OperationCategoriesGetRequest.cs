using MyPiggyBank.Core.Protocol.Base;

namespace MyPiggyBank.Core.Protocol.OperationCategories.Requests
{
    public class OperationCategoriesGetRequest : QueryStringParams
    {
        public string Name { get; set; }
    }
}
