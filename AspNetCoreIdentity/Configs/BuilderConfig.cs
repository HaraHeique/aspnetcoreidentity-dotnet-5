using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreIdentity.Configs
{
    public static class BuilderConfig
    {
        public static IConfiguration Build(this ConfigurationBuilder builderConfig, IWebHostEnvironment hostingEnvironment) 
        {
            var builder = builderConfig
                .SetBasePath(hostingEnvironment.ContentRootPath) // A partir do diretório atual para frente
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables(); // Coisas do asp.net core que utiliza para trabalhar

            // Caso ambiente de produção trabalhar com user secrets (pegar a info de outro local -> Manage User Secrets)
            if (hostingEnvironment.IsProduction())
            {
                builder.AddUserSecrets<Startup>();
            }

            return builder.Build();
        }
    }
}
