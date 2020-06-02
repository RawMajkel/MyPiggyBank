using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPiggyBank.Data;
using MyPiggyBank.Data.Repository;
using MyPiggyBank.Web.Configuration;

namespace MyPiggyBank.Integration.Test
{
    public class StartupTest
    {
        public IConfiguration Configuration { get; }

        public StartupTest(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStringPiggyTest = Configuration.GetConnectionString(nameof(MyPiggyBankContext));
            
            if (!string.IsNullOrEmpty(connectionStringPiggyTest))
            {
                services.ConfigureMyPiggyBankContext(Configuration);
            }
            else
            {
                var dbRuntime = DbHelper.CreateDbInRuntimeMemory();
                services.AddSingleton<MyPiggyBankContext>(_ => dbRuntime);
            }

            services
                .Configure(Configuration)
                .AddApplicationPart(Assembly.Load(new AssemblyName("MyPiggyBank.Web")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StartupExtension.ConfigureApp(app, env);
        }
    }
}
