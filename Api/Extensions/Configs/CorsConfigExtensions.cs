using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Extensions.Configs
{
    public static class CorsConfigExtensions
    {
        public static IServiceCollection AddCorsExtensions(
            this IServiceCollection services
        )
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            return services;
        }
    }
}