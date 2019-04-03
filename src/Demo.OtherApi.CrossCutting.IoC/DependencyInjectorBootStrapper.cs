using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Demo.OtherApi.BusinessLogic.Core.Services;
using Demo.OtherApi.BusinessLogic.Core.Services.Interfaces;
using Demo.OtherApi.BusinessLogic.Interfaces;
using Demo.OtherApi.BusinessLogic.Services;
using Demo.OtherApi.BusinessLogic.Services.Interfaces;
using Demo.OtherApi.CrossCutting.Identity;
using Demo.OtherApi.DataAccess.Core.Interfaces;
using Demo.OtherApi.DataAccess.DynamoDb.Services;
using System;

namespace Demo.OtherApi.CrossCutting.IoC
{
    public class DependencyInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services,
                                            IHostingEnvironment env, 
                                            IConfiguration configuration)
        {
            // Business Logic
            services.AddScoped<IAppSettingQueryService, AppSettingQueryService>();
            services.AddScoped<IAppSettingService, AppSettingService>();

            services.AddScoped<IBusinessManagerService, BusinessManagerService>();

            // Data Access
            services.AddScoped<IAppSettingDataService, AppSettingDynamoDbDataService>();

            // CrossCutting
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
