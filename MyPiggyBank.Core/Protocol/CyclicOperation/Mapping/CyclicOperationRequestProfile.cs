using AutoMapper;
using MyPiggyBank.Core.Protocol.CyclicOperation.Requests;
using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Core.Protocol.CyclicOperation.Mapping
{
    public class CyclicOperationRequestProfile : Profile
    {
        public CyclicOperationRequestProfile()
        {
            CreateMap<CyclicOperationRequest, Data.Model.CyclicOperation>()
                .ForMember(d => d.OperationCategory, opt => opt.Ignore())
                .ForMember(d => d.Resource, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
