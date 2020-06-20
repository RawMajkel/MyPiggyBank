using AutoMapper;

namespace MyPiggyBank.Core.Protocol.Resource
{
    public class ResourceProfile : Profile 
    {
        public ResourceProfile()
        {
            CreateMap<Data.Model.Resource, ResourceInfo>();
        }
    }
}
