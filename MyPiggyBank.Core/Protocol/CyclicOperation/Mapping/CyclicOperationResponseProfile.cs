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
            CreateMap<Data.Model.CyclicOperation, CyclicOperationResponse>()
                .ForMember(d => d.MinEstimatedValue, opt => opt.Ignore())
                .ForMember(d => d.MaxEstimatedValue, opt => opt.Ignore())
                .ForMember(d => d.MinPeriod, opt => opt.Ignore())
                .ForMember(d => d.MaxPeriod, opt => opt.Ignore())
                .ForMember(d => d.MinNextOccurence, opt => opt.Ignore())
                .ForMember(d => d.MaxNextOccurence, opt => opt.Ignore())
                .ForMember(d => d.Limit, opt => opt.Ignore())
                .ForMember(d => d.Page, opt => opt.Ignore())
                .ForMember(d => d.User, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
