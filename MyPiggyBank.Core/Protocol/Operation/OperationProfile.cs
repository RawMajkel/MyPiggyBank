using AutoMapper;

namespace MyPiggyBank.Core.Protocol.Operation
{
    public class OperationProfile : Profile
    {
        public OperationProfile()
        {
            CreateMap<Data.Model.Operation, OperationInfo>();
        }
    }
}
