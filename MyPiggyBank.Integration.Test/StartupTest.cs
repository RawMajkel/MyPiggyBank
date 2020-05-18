using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPiggyBank.Data;
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

            Action action = !string.IsNullOrEmpty(connectionStringPiggyTest)
                ? (Action) (() =>
                {
                    services.ConfigureMyPiggyBankContext(Configuration);
                })
                : () =>
                {
                    var dbRuntime = DbHelper.CreateDbInRuntimeMemory();
                    services.AddTransient<MyPiggyBankContext>(_ => dbRuntime);
                };

            action.Invoke();
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
