using System.Net;
using System.Text;
using System.Text.Json;
using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.Entities;
using minimal_api.Dominios.Enums;
using minimal_api.Dominios.ModelViews;
using Test.Helpers;
using Test.Requests.Interfaces;
namespace Test.Requests
{
    [TestClass]
    public class AdministradorRequestTest : IAdministradorRequestTest
    {
        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            Setup.ClassInit(testContext);
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
            Setup.ClassCleanup();
        }
        [TestMethod]
        public async Task TestandoLogin()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                Email = "adm@teste.com",
                Senha = "123456"
            };

            var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");

            // Act
            var response = await Setup.client.PostAsync("/login", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();
            var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.IsNotNull(admLogado?.Email ?? "");
            Assert.IsNotNull(admLogado?.Perfil ?? "");
            Assert.IsNotNull(admLogado?.Token ?? "");

            Console.WriteLine(admLogado?.Token);
        }
        [TestMethod]
        public async Task BuscarResgistros()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");

            // Act
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var responseLista = await Setup.client.GetAsync("/administradores?pagina=1");
            responseLista.EnsureSuccessStatusCode();
            var resultLista = await responseLista.Content.ReadAsStringAsync();
            var listaAdms = JsonSerializer.Deserialize<List<Administrador>>(
                resultLista,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            //Asserts
            Assert.IsNotNull(listaAdms, "A lista de administradores não pode ser nula");
            Assert.IsTrue(listaAdms.Count > 0, "A lista de administradores deve conter pelo menos 1 items");
            Assert.IsTrue(listaAdms.Count == 2, "A lista de administradores deve conter no maximo 2 items");
        }
        [TestMethod]
        public async Task BuscarRegistroPorId()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            //Act
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var responseBusca = await Setup.client.GetAsync("/administradores/1");
            responseBusca.EnsureSuccessStatusCode();
            var resultBusca = await responseBusca.Content.ReadAsStringAsync();
            var adm = JsonSerializer.Deserialize<Administrador>(
                resultBusca,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            //Assert
            Assert.IsNotNull(adm, "O administrador retornado não pode ser nulo");
            Assert.AreEqual(1, adm.Id);
            Assert.AreEqual("adm@teste.com", adm.Email);
            Assert.AreEqual("Adm", adm.Perfil);
        }
        [TestMethod]
        public async Task BuscarRegistroInexistente()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            //Act
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var responseBusca = await Setup.client.GetAsync("/administradores/5");
            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, responseBusca.StatusCode);
        }
        [TestMethod]
        public async Task AtualizarRegistro()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            var administrador = new AdministradorDTO
            {
                Email = "editorAtualizado@teste.com",
                Perfil = Perfil.Editor,
                Senha = "123456"
            };
            var content = new StringContent(
                JsonSerializer.Serialize(administrador),
                Encoding.UTF8,
                "application/json"
            );
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            //Act
            var responseAtualizacao = await Setup.client.PutAsync("/administradores/2", content);
            Assert.AreEqual(HttpStatusCode.OK, responseAtualizacao.StatusCode);
            responseAtualizacao.EnsureSuccessStatusCode();
            var resultAtualizacao = await responseAtualizacao.Content.ReadAsStringAsync();
            var adm = JsonSerializer.Deserialize<Administrador>(
                resultAtualizacao,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            //Asserts
            Assert.IsNotNull(adm, "O administrador retornado não pode ser nulo");
            Assert.AreEqual("editorAtualizado@teste.com", adm.Email);
        }
        [TestMethod]
        public async Task AtualizarRegistroInexistente()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            var administrador = new AdministradorDTO
            {
                Email = "editorAtualizado@teste.com",
                Perfil = Perfil.Editor,
                Senha = "123456"
            };
            var content = new StringContent(
                JsonSerializer.Serialize(administrador),
                Encoding.UTF8,
                "application/json"
            );
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            //Act
            var responseAtualizacao = await Setup.client.PutAsync("/administradores/5", content);

            //Asserts
            Assert.AreEqual(HttpStatusCode.NotFound, responseAtualizacao.StatusCode);
        }
        [TestMethod]
        public async Task AtualizarRegistroInvalido()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            var administrador = new AdministradorDTO
            {
                Email = "",
                Perfil = Perfil.Editor,
                Senha = ""
            };
            var content = new StringContent(
                JsonSerializer.Serialize(administrador),
                Encoding.UTF8,
                "application/json"
            );
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            //Act
            var responseAtualizacao = await Setup.client.PutAsync("/administradores/5", content);

            //Asserts
            Assert.AreEqual(HttpStatusCode.BadRequest, responseAtualizacao.StatusCode);
        }
        [TestMethod]
        public async Task CriarRegistro()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            var administrador = new AdministradorDTO
            {
                Email = "admcriado@teste.com",
                Perfil = Perfil.Adm,
                Senha = "123456"
            };
            var content = new StringContent(
                JsonSerializer.Serialize(administrador),
                Encoding.UTF8,
                "application/json"
            );
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            //Act
            var responseCriacao = await Setup.client.PostAsync("/administradores", content);
            var resultCriacao = await responseCriacao.Content.ReadAsStringAsync();
            var adm = JsonSerializer.Deserialize<Administrador>(
                resultCriacao,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            //Asserts
            Assert.AreEqual(HttpStatusCode.Created, responseCriacao.StatusCode);
            Assert.AreEqual(administrador.Email, adm.Email);
            Assert.AreEqual(administrador.Perfil.ToString(), adm.Perfil);
        }
        [TestMethod]
        public async Task CriarRegistroInvalido()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            var administrador = new AdministradorDTO
            {
                Email = "",
                Perfil = Perfil.Adm,
                Senha = ""
            };
            var content = new StringContent(
                JsonSerializer.Serialize(administrador),
                Encoding.UTF8,
                "application/json"
            );
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            //Act
            var responseCriacao = await Setup.client.PostAsync("/administradores", content);
            
            //Asserts
            Assert.AreEqual(HttpStatusCode.BadRequest, responseCriacao.StatusCode);

        }
        [TestMethod]
        public async Task CriarPorEmailDuplicado()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            var administrador = new AdministradorDTO
            {
                Email = "adm@teste.com",
                Perfil = Perfil.Adm,
                Senha = "123456"
            };
            var content = new StringContent(
                JsonSerializer.Serialize(administrador),
                Encoding.UTF8,
                "application/json"
            );
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            //Act
            var responseCriacao = await Setup.client.PostAsync("/administradores", content);

            //Asserts
            Assert.AreEqual(HttpStatusCode.Conflict, responseCriacao.StatusCode);
        }
        [TestMethod]
        public async Task DeletarRegistro()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            //Act
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var responseDelete = await Setup.client.DeleteAsync("/administradores/2");
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, responseDelete.StatusCode);
        }
        [TestMethod]
        public async Task DeletarRegistroInexistente()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            //Act
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var responseDelete = await Setup.client.DeleteAsync("/administradores/5");
            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, responseDelete.StatusCode);
        }

        
    }
}