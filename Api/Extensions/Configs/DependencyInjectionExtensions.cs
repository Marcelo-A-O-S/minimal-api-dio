using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominios.Interfaces;
using minimal_api.Dominios.Services;

namespace minimal_api.Extensions.Configs
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services
        )
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAdministradorService, AdministradorService>();
            services.AddScoped<IVeiculoService, VeiculoService>();
            return services;
        }
    }
}