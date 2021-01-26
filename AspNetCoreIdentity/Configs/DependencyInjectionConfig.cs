using AspNetCoreIdentity.Extensions;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentity.Configs
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Pegar em qualquer momento o HTTP context da app
            services.AddScoped((context) => Logger.Factory.Get()); // Configuração do Kiss log

            services.AddScoped<AuditoriaFilter>(); // Injeção da dependência do filtro

            return services;
        }
    }
}
