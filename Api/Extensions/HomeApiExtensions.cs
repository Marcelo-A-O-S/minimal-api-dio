using minimal_api.Dominios.ModelViews;
namespace minimal_api.extensions
{
    public static class HomeApiExtensions
    {
        public static IEndpointRouteBuilder MapHomeApi(
            this IEndpointRouteBuilder app
        )
        {
            app.MapGet("/", () => new Home()).AllowAnonymous().WithTags("Home").WithDescription("Endpoint inicial.");
            return app;
        }
    }
}