using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                    .AddControllers();
        }
        private static IServiceCollection ConfigureRepositories(this IServiceCollection service)
        {
            return service.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
