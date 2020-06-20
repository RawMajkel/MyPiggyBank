using AutoMapper;

namespace MyPiggyBank.Core.Protocol.OperationCategories
{
    public class OperationCategoryProfile : Profile
    {
        public OperationCategoryProfile()
        {
            CreateMap<Data.Model.OperationCategory, OperationCategoryInfo>();
        }
    }
}
