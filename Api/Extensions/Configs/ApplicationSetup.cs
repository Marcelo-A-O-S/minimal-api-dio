namespace minimal_api.Extensions.Configs
{
    public static class ApplicationSetup
    {
        public static IServiceCollection AddApplicationSetup(
            this IServiceCollection services, IConfiguration configuration
        )
        {
            services.AddJwtAuthentication(configuration);
            services.AddAuthorization();
            services.AddApplicationServices();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerWithJwt();
            services.AddDBContexto(configuration);
            services.AddCorsExtensions();
            return services;
        }
    }
}