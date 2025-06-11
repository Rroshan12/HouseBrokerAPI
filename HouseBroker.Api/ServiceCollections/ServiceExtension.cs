using HouseBroker.Infra.Helpers;
using HouseBroker.Infra.Interface;
using HouseBroker.Infra.Repository;
using HouseBroker.Infra.Services;

namespace HouseBroker.Api.ServiceCollections
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServiceDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IPropertyListingRepository, PropertyListingRepository>();
            services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();


            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IPropertyListingService, PropertyListingService>();

            return services;
        }
    }
}
