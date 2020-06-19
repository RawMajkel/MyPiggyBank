using MyPiggyBank.Core.Protocol.Base;

namespace MyPiggyBank.Core.Protocol.OperationCategories
{
    public class OperationCategoriesQuery : QueryStringParams 
    {
        public string Name { get; set; }
    }
}
