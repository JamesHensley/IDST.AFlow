using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace IDST.AFlow.Browser.UI.Extensions
{
    public static class ServiceExtensions
    {
        // Extension method
        public static IServiceCollection AddMapster(this IServiceCollection services, Action<TypeAdapterConfig> options = null)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetAssembly(typeof(Program)));

            options?.Invoke(config);

            services.AddSingleton(config);
            //services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
