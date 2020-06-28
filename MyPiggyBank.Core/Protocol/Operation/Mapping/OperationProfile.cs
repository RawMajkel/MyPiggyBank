using AutoMapper;
using MyPiggyBank.Core.Protocol.Operation.Requests;
using MyPiggyBank.Core.Protocol.Operation.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.Operation.Mapping
{
    public class OperationProfile : Profile
    {
        public OperationProfile()
        {
            CreateMap<OperationSaveRequest, Data.Model.Operation>()
                .ForMember(dst => dst.Id, opt => opt.Condition(src => src.Id != Guid.Empty))
                .ForMember(d => d.OperationCategory, opt => opt.Ignore())
                .ForMember(d => d.Resource, opt => opt.Ignore());
            CreateMap<Data.Model.Operation, OperationResponse>();
        }

    }
}
