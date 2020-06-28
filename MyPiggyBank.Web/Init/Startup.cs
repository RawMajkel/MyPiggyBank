using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyPiggyBank.Web {
    public class Startup {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services.ConfigureMyPiggyBankContext(Configuration).Configure(Configuration);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => StartupExtension.ConfigureApp(app, env);
    }
}
