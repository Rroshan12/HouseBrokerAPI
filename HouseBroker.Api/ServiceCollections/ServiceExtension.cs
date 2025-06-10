using HouseBroker.Infra.Interface;
using HouseBroker.Infra.Repository;

namespace HouseBroker.Api.ServiceCollections
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServiceDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
