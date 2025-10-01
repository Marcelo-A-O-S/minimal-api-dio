using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.Interfaces;
using minimal_api.Dominios.ModelViews;
using Test.Mocks;
namespace Test.Helpers
{
    public class Setup
    {
        public const string PORT = "5001";
        public static TestContext testContext;
        public static WebApplicationFactory<Program> http;
        public static HttpClient client;

        public static void ClassInit(TestContext testContext)
        {
            Setup.testContext = testContext;
            Setup.http = new WebApplicationFactory<Program>();
            Setup.http = Setup.http.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", Setup.PORT).UseEnvironment("Development");
                builder.ConfigureServices((context, services) =>
                {
                    services.AddScoped<IAdministradorService, AdministradorServiceMock>();
                    services.AddScoped<IVeiculoService, VeiculoServiceMock>();
                });
            });
            Setup.client = Setup.http.CreateClient();
        }
        public static void ClassCleanup()
        {
            Setup.http.Dispose();
        }
        public static async Task<string> LoginAsync()
        {
            var loginDTO = new LoginDTO
            {
                Email = "adm@teste.com",
                Senha = "123456"
            };
            var contentLogin = new StringContent(
                JsonSerializer.Serialize(loginDTO),
                Encoding.UTF8,
                "application/json");
            var responseLogin = await Setup.client.PostAsync("/login", contentLogin);
            responseLogin.EnsureSuccessStatusCode();
            var resultLogin = await responseLogin.Content.ReadAsStringAsync();
            var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(
                resultLogin,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            return admLogado.Token;
        }
    }
}