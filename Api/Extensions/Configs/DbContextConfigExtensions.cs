using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api.Infraestrutura.DB;

namespace minimal_api.Extensions.Configs
{
    public static class DbContextConfigExtensions
    {
        public static IServiceCollection AddDBContexto(
            this IServiceCollection services, IConfiguration configuration
        )
        {
            services.AddDbContext<DBContexto>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("mysql"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("mysql")));
            });
            return services;
        }
    }
}