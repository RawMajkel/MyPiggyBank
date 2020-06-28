using AutoMapper;
using MyPiggyBank.Core.Protocol.Resource.Requests;
using MyPiggyBank.Core.Protocol.Resource.Responses;
using System;

namespace MyPiggyBank.Core.Protocol.Resource.Mapping
{
    public class ResourceProfile : Profile
    {
        public ResourceProfile()
        {
            CreateMap<ResourceSaveRequest, Data.Model.Resource>()
                .ForMember(dst => dst.Id, opt => opt.Condition(src => src.Id != Guid.Empty))
                .ForMember(dst => dst.User, opt => opt.Ignore())
                .ForMember(dst => dst.Operations, opt => opt.Ignore())
                .ForMember(dst => dst.CyclicOperations, opt => opt.Ignore());

            CreateMap<Data.Model.Resource, ResourceResponse>();
        }
    }
}