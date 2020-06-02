using AutoMapper;
using MyPiggyBank.Core.Protocol.Account.DTO;
using MyPiggyBank.Core.Protocol.Account.Requests;
using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Core.Protocol.Account.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.PasswordHash, opt => opt.Ignore())
                .ForMember(d => d.OperationCategories, opt => opt.Ignore())
                .ForMember(d => d.Resources, opt => opt.Ignore());

            CreateMap<User, AccountInfo>();
        }
    }
}
