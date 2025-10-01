using System.Net;
using System.Text;
using System.Text.Json;
using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.Entities;
using Test.Helpers;
using Test.Requests.Interfaces;

namespace Test.Requests
{
    [TestClass]
    public class VeiculoRequestTest : IRequestTest
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
        public async Task BuscarResgistros()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            //Act
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var responseLista = await Setup.client.GetAsync("/veiculos?pagina=1");
            responseLista.EnsureSuccessStatusCode();
            var resultLista = await responseLista.Content.ReadAsStringAsync();
            var listaVeiculos = JsonSerializer.Deserialize<List<Veiculo>>(
                resultLista,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
            //Asserts 
            Assert.IsNotNull(listaVeiculos, "A lista de veiculos não pode ser nula");
            Assert.IsTrue(listaVeiculos.Count > 0, "A lista de veiculos deve conter pelo menos 1 items");
            Assert.IsTrue(listaVeiculos.Count == 3, "A lista de veiculos deve conter no maximo 3 items");
        }
        [TestMethod]
        public async Task BuscarRegistroPorId()
        {
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            //Act
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var responseBusca = await Setup.client.GetAsync("/veiculos/1");
            responseBusca.EnsureSuccessStatusCode();
            var resultBusca = await responseBusca.Content.ReadAsStringAsync();
            var veiculo = JsonSerializer.Deserialize<Veiculo>(
                resultBusca,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
            //Assert
            Assert.IsNotNull(veiculo, "O veiculo retornado não pode ser nulo");
            Assert.AreEqual(1, veiculo.Id);
            Assert.AreEqual("Chevrolet", veiculo.Marca);
            Assert.AreEqual("Onix", veiculo.Nome);
            Assert.AreEqual(2005, veiculo.Ano);
        }
        [TestMethod]
        public async Task BuscarRegistroInexistente()
        {
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            //Act
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var responseBusca = await Setup.client.GetAsync("/veiculos/5");

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, responseBusca.StatusCode);
        }
        [TestMethod]
        public async Task AtualizarRegistro()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            var veiculo = new VeiculoDTO
            {
                Ano = 2005,
                Marca = "Chevrolet",
                Nome = "Onix 2.0"
            };
            var content = new StringContent(
                JsonSerializer.Serialize(veiculo),
                Encoding.UTF8,
                "application/json"
            );
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            //Act
            var responseAtualizacao = await Setup.client.PutAsync("/veiculos/2", content);
            responseAtualizacao.EnsureSuccessStatusCode();
            var resultAtualizacao = await responseAtualizacao.Content.ReadAsStringAsync();
            var veiculoAtualizado = JsonSerializer.Deserialize<Veiculo>(
                resultAtualizacao,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            //Asserts
            Assert.IsNotNull(veiculoAtualizado, "O veiculo retornado não pode ser nulo");
            Assert.AreEqual(veiculo.Nome, veiculoAtualizado.Nome);
        }
        [TestMethod]
        public async Task AtualizarRegistroInexistente()
        {
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            var veiculo = new VeiculoDTO
            {
                Ano = 2005,
                Marca = "Chevrolet",
                Nome = "Onix 2.0"
            };
            var content = new StringContent(
                JsonSerializer.Serialize(veiculo),
                Encoding.UTF8,
                "application/json"
            );
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            //Act
            var responseAtualizacao = await Setup.client.PutAsync("/veiculos/5", content);

            //Asserts
            Assert.AreEqual(HttpStatusCode.NotFound, responseAtualizacao.StatusCode);
        }
        [TestMethod]
        public async Task AtualizarRegistroInvalido()
        {
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            var veiculo = new VeiculoDTO
            {
                Ano = 0,
                Marca = "",
                Nome = ""
            };
            var content = new StringContent(
                JsonSerializer.Serialize(veiculo),
                Encoding.UTF8,
                "application/json"
            );
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            //Act
            var responseAtualizacao = await Setup.client.PutAsync("/veiculos/5", content);

            //Asserts
            Assert.AreEqual(HttpStatusCode.BadRequest, responseAtualizacao.StatusCode);
        }
        [TestMethod]
        public async Task CriarRegistro()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            var veiculo = new VeiculoDTO
            {
                Nome = "Gol 1.6",
                Marca = "Chevrolet",
                Ano = 2000
            };
            var content = new StringContent(
                JsonSerializer.Serialize(veiculo),
                Encoding.UTF8,
                "application/json"
            );
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            //Act
            var responseCriacao = await Setup.client.PostAsync("/veiculos", content);
            var resultCriacao = await responseCriacao.Content.ReadAsStringAsync();
            var veiculoRetorno = JsonSerializer.Deserialize<Veiculo>(
                resultCriacao,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            //Asserts
            Assert.AreEqual(HttpStatusCode.Created, responseCriacao.StatusCode);
            Assert.AreEqual(veiculo.Nome, veiculoRetorno.Nome);
            Assert.AreEqual(veiculo.Marca, veiculoRetorno.Marca);
            Assert.AreEqual(veiculo.Ano, veiculoRetorno.Ano);
        }
        [TestMethod]
        public async Task CriarRegistroInvalido()
        {
            //Arrange
            var token = await Setup.LoginAsync();
            Assert.IsNotNull(token, "Token não foi retornado pelo login!");
            var veiculo = new VeiculoDTO
            {
                Nome = "",
                Marca = "",
                Ano = 0
            };
            var content = new StringContent(
                JsonSerializer.Serialize(veiculo),
                Encoding.UTF8,
                "application/json"
            );
            Setup.client.DefaultRequestHeaders.Clear();
            Setup.client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            //Act
            var responseCriacao = await Setup.client.PostAsync("/veiculos", content);
            
            //Asserts
            Assert.AreEqual(HttpStatusCode.BadRequest, responseCriacao.StatusCode);
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
            var responseDelete = await Setup.client.DeleteAsync("/veiculos/3");
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
            var responseDelete = await Setup.client.DeleteAsync("/veiculos/5");
            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, responseDelete.StatusCode);
        }
    }
}