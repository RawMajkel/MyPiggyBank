using AutoMapper;
using MyPiggyBank.Core.Protocol.CyclicOperation.Requests;
using MyPiggyBank.Core.Protocol.CyclicOperation.Responses;

namespace MyPiggyBank.Core.Protocol.CyclicOperation.Mapping
{
    public class CyclicOperationProfile : Profile
    {
        public CyclicOperationProfile()
        {
            CreateMap<CyclicOperationSaveRequest, Data.Model.CyclicOperation>()
                   .ForMember(d => d.OperationCategory, opt => opt.Ignore())
                   .ForMember(d => d.Resource, opt => opt.Ignore())
                   .ForMember(d => d.Description, opt => opt.Ignore())
                   .ReverseMap();

            CreateMap<Data.Model.CyclicOperation, CyclicOperationResponse>();
        }
    }
}
