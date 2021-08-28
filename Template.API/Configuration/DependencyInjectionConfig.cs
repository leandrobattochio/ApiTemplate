using Template.Core.Messages;
using Template.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Template.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Extensão para centralizar em um unico lugar as injeções de dependencia
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IMediatorHandler, MediatorHandler>();
            services.AddTransient<ICepService, CepService>();

            return services;
        }

    }
}