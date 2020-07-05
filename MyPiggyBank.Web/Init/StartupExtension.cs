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
using MyPiggyBank.Core.Protocol.Base;
using MyPiggyBank.Core.Protocol.CyclicOperation.Mapping;
using MyPiggyBank.Core.Protocol.CyclicOperation.Validators;
using MyPiggyBank.Core.Protocol.Operation.Mapping;
using MyPiggyBank.Core.Protocol.Operation.Validators;
using MyPiggyBank.Core.Protocol.OperationCategories.Mapping;
using MyPiggyBank.Core.Protocol.OperationCategories.Validators;
using MyPiggyBank.Core.Protocol.Resource.Mapping;
using MyPiggyBank.Core.Protocol.Resource.Validators;
using MyPiggyBank.Core.Service;
using MyPiggyBank.Core.Service.Implementation;
using MyPiggyBank.Data;
using MyPiggyBank.Data.Model;
using MyPiggyBank.Data.Repository;
using System;
using System.Text;

namespace MyPiggyBank.Web
{
    public static class StartupExtension
    {
        public static void ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection()
               .UseRouting()
               .UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                )
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints => endpoints.MapControllers());
        }

        public static IServiceCollection ConfigureMyPiggyBankContext(this IServiceCollection service, IConfiguration configuration) => service
            .AddDbContext<MyPiggyBankContext>(opt => opt
            .UseLazyLoadingProxies()
            .UseSqlServer(configuration.GetConnectionString(nameof(MyPiggyBankContext))));
        
        public static IMvcBuilder Configure(this IServiceCollection service, IConfiguration configuration) => service
            .ConfigureJwtToken(configuration)
            .RegisterProfiles()
            .RegisterServices()
            //.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            //{
            //    builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
            //}))
            .AddControllers()
            .RegisterValidators();

        private static IServiceCollection ConfigureRepositories(this IServiceCollection service) => service
            .AddTransient<IUsersRepository, UsersRepository>()
            .AddTransient<IResourcesRepository, ResourcesRepository>()
            .AddTransient<IOperationCategoriesRepository, OperationCategoriesRepository>()
            .AddTransient<ICyclicOperationRepository, CyclicOperationsRepository>()
            .AddTransient<IOperationsRepository, OperationsRepository>();

        private static IServiceCollection RegisterServices(this IServiceCollection service) => service
            .ConfigureRepositories()
            .AddTransient<IJwtService, JwtService>()
            .AddTransient<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddTransient<IAccountsService, AccountsService>()
            .AddTransient<IOperationCategoriesService, OperationCategoriesService>()
            .AddTransient<IResourcesService, ResourcesService>()
            .AddTransient<ICyclicOperationsService, CyclicOperationsService>()
            .AddTransient<IOperationsService, OperationsService>();
        private static IMvcBuilder RegisterValidators(this IMvcBuilder builder) => builder.AddFluentValidation(fv => fv
            .RegisterValidatorsFromAssemblyContaining<RegisterRequestValidator>()
            .RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>()
            .RegisterValidatorsFromAssemblyContaining<QueryStringParamsValidator<QueryStringParams>>()
            .RegisterValidatorsFromAssemblyContaining<ResourceSaveRequestValidator>()
            .RegisterValidatorsFromAssemblyContaining<OperationRequestValidator>()
            .RegisterValidatorsFromAssemblyContaining<CyclicOperationRequestValidator>()
            .RegisterValidatorsFromAssemblyContaining<OperationCategoriesRequestValidator>());

        private static IServiceCollection RegisterProfiles(this IServiceCollection service) => service
            .AddSingleton(
                new MapperConfiguration(mc => {
                    mc.AddProfile<AccountProfile>();
                    mc.AddProfile<OperationProfile>();
                    mc.AddProfile<CyclicOperationProfile>();
                    mc.AddProfile<OperationCategoriesProfile>();
                    mc.AddProfile<ResourceProfile>();
                }).CreateMapper());

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
                        ValidateLifetime = true,
                        ValidIssuer = configuration["Authorization:Issuer"]
                    };
                });

            return service;
        }
    }
}
