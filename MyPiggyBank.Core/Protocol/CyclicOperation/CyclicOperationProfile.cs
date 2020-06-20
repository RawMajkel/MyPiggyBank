using AutoMapper;

namespace MyPiggyBank.Core.Protocol.CyclicOperation
{
    public class CyclicOperationProfile : Profile
    {
        public CyclicOperationProfile()
        {
            CreateMap<Data.Model.CyclicOperation, CyclicOperationInfo>();
        }
    }
}
