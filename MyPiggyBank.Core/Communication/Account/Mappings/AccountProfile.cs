using AutoMapper;
using MyPiggyBank.Core.Communication.Account.Requests;
using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Core.Communication.Account.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.PasswordHash, opt => opt.Ignore())
                .ForMember(d => d.OperationCategories, opt => opt.Ignore())
                .ForMember(d => d.Resources, opt => opt.Ignore());
        }
    }
}
