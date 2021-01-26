using AspNetCoreIdentity.Areas.Identity.Data;
using AspNetCoreIdentity.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentity.Configs
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            // Checando se é necessário o consentimento
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Adicionando o contexto do identity
            services.AddDbContext<AspNetCoreIdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AspNetCoreIdentityContextConnection"))
            );

            // Adicionando o default identity
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>() // Para adicionar as roles do usuários da aplicação (para autorização)
                .AddDefaultUI() // Usa as telas do Razor referente aos usuários da aplicação
                .AddEntityFrameworkStores<AspNetCoreIdentityContext>(); // Adiciona as implementações do identity com o EF Core

            return services;
        }

        // Configuração das claims
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeEditar", policy => policy.RequireClaim("PodeEditar"));

                options.AddPolicy("PodeLer", policy => policy.Requirements.Add(new PermissaoNecessario("PodeLer")));
                options.AddPolicy("PodeEscrever", policy => policy.Requirements.Add(new PermissaoNecessario("PodeEscrever")));
            });

            return services;
        }
    }
}
