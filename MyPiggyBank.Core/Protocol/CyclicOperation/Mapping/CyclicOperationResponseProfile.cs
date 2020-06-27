using AutoMapper;
using MyPiggyBank.Core.Protocol.CyclicOperation.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.CyclicOperation.Mapping
{
    public class CyclicOperationResponseProfile : Profile
    {
        public CyclicOperationResponseProfile()
        {
            CreateMap<CyclicOperationResponse, Data.Model.CyclicOperation>()
                .ForMember(d => d.OperationCategory, opt => opt.Ignore())
                .ForMember(d => d.Resource, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
