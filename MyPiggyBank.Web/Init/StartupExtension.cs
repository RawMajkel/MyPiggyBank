using System;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MyPiggyBank.Core.Protocol.Account.Mappings;
using MyPiggyBank.Core.Protocol.Account.Validators;
using MyPiggyBank.Core.Protocol.Query.Validators;
using MyPiggyBank.Core.Service;
using MyPiggyBank.Data;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;

namespace MyPiggyBank.Web
{
    public static class StartupExtension
    {
        public static void ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(c =>
            {
                c.AllowAnyOrigin();
                c.AllowAnyHeader();
                c.AllowAnyMethod();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public static IServiceCollection ConfigureMyPiggyBankContext(this IServiceCollection service, IConfiguration configuration)
        {
            return service.AddDbContext<MyPiggyBankContext>(opt =>
                opt.UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString(nameof(MyPiggyBankContext))));
        }
        public static IMvcBuilder Configure(this IServiceCollection service, IConfiguration configuration)
        {
            return service
                    .ConfigureJwtToken(configuration)
                    .RegisterProfiles()
                    .RegisterServices()
                    .AddControllers()
                    .RegisterValidators();
        }

        private static IServiceCollection RegisterServices(this IServiceCollection service)
        {
            return service.ConfigureRepositories()
                          .AddTransient<IJwtService, JwtService>()
                          .AddTransient<IPasswordHasher<User>, PasswordHasher<User>>()
                          .AddTransient<IAccountsService, AccountsService>()
                          .AddTransient<IResourcesService, ResourcesService>()
                          .AddTransient<IOperationsService, OperationsService>();
        }

        private static IServiceCollection ConfigureRepositories(this IServiceCollection service)
        {
            return service
                .AddTransient<IUsersRepository, UsersRepository>()
                .AddTransient<IResourcesRepository, ResourcesRepository>()
                .AddTransient<IOperationsRepository, OperationsRepository>();
        }

        private static IServiceCollection RegisterProfiles(this IServiceCollection service)
        {
            var mappingConf = new MapperConfiguration(mc => { mc.AddProfile<AccountProfile>(); });
            IMapper mapper = mappingConf.CreateMapper();

            service.AddSingleton(mapper);

            return service;
        }

        private static IServiceCollection ConfigureJwtToken(this IServiceCollection service, IConfiguration configuration)
        {
            var secretKey = configuration["Authorization:SecretKey"];

            if (string.IsNullOrEmpty(secretKey))
                throw new ArgumentNullException("Jwt secret key is empty.");

            service
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });

            return service;
        }

        private static IMvcBuilder RegisterValidators(this IMvcBuilder builder)
        { 
            return builder.AddFluentValidation(fv => {
                fv.RegisterValidatorsFromAssemblyContaining<RegisterRequestValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<QueryStringParamsValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<ResourcesQueryValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<OperationsQueryValidator>();
            });
        }
    }
}
