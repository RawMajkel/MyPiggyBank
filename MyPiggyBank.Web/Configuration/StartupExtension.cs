using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPiggyBank.Core.Communication.Account.Mappings;
using MyPiggyBank.Data;
using MyPiggyBank.Data.Repositories.Interfaces;
using MyPiggyBank.Data.Repositories.Models;

namespace MyPiggyBank.Web.Configuration
{
    public static class StartupExtension
    {
        public static void Configure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<MyPiggyBankContext>(opt =>
                        opt.UseLazyLoadingProxies()
                            .UseSqlServer(configuration.GetConnectionString(nameof(MyPiggyBankContext))))
                    .RegisterProfiles()
                    .AddControllers();
        }
        private static IServiceCollection ConfigureRepositories(this IServiceCollection service)
        {
            return service.AddTransient<IUserRepository, UserRepository>();
        }

        private static IServiceCollection RegisterProfiles(this IServiceCollection service)
        {
            var mappingConf = new MapperConfiguration(mc => { mc.AddProfile<AccountProfile>(); });
            IMapper mapper = mappingConf.CreateMapper();

            service.AddSingleton(mapper);

            return service;
        }
    }
}
