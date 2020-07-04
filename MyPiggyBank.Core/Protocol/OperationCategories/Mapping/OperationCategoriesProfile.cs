using AutoMapper;
using MyPiggyBank.Core.Protocol.OperationCategories.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.OperationCategories.Mapping
{
    public class OperationCategoriesProfile : Profile
    {
        public OperationCategoriesProfile()
        {
            CreateMap<OperationCategoriesSaveRequest, Data.Model.OperationCategory>()
                   .ForMember(dst => dst.Id, opt => opt.Condition(src => src.Id != Guid.Empty))
                   .ForMember(d => d.User, opt => opt.Ignore())
                   .ForMember(d => d.UserId, opt => opt.Ignore())
                   .ForMember(d => d.Operations, opt => opt.Ignore())
                   .ForMember(d => d.CyclicOperations, opt => opt.Ignore());
            CreateMap<Data.Model.OperationCategory, OperationCategoriesSaveRequest>();
        }
    }
}
