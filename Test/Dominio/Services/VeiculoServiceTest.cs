using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominios.Entities;
using minimal_api.Dominios.Services;
using Test.Dominio.Services.Interfaces;
using Test.Infraestrutura;

namespace Test.Dominio.Services
{
    [TestClass]
    public class VeiculoServiceTest : ITestServices
    {
        [TestInitialize]
        public void Setup()
        {
            using var contexto = TestDBContexto.Create();
            contexto.Database.EnsureDeleted();
            contexto.Database.EnsureCreated();
        }

        [TestMethod]
        public void Salvar()
        {
            //Range
            var veiculo = new Veiculo();
            veiculo.Nome = "Corolla";
            veiculo.Marca = "Toyota";
            veiculo.Ano = 2020;
            using var contexto = TestDBContexto.Create();
            var veiculoService = new VeiculoService(contexto);
            var quantidadeTotal = veiculoService.Listar(1).Count();
            //Act
            veiculoService.Incluir(veiculo);
            //Assert
            Assert.AreEqual(quantidadeTotal + 1, veiculoService.Listar(1).Count());
        }
        
        [TestMethod]
        public void SalvarObjetoInvalido()
        {
            //Range
            var veiculo = new Veiculo();
            veiculo.Nome = "Onix";
            veiculo.Marca = "Chevrolet";
            veiculo.Ano = 2005;
            using var contexto = TestDBContexto.Create();
            var veiculoService = new VeiculoService(contexto);
            //Act
            var ex = Assert.ThrowsException<InvalidOperationException>(() =>
            {
                veiculoService.Incluir(veiculo);
            });
            //Assert
            Assert.AreEqual("Já existe veículo com esse nome.", ex.Message);
        }
        [TestMethod]
        public void SalvarObjetoVazio()
        {
            //Range
            using var contexto = TestDBContexto.Create();
            var veiculoService = new VeiculoService(contexto);
            //Act
            var ex = Assert.ThrowsException<InvalidOperationException>(() =>
            {
                veiculoService.Incluir(null);
            });
            //Assert
            Assert.AreEqual("O veículo não pode ser nulo.", ex.Message);
        }
        [TestMethod]
        public void BuscarPorId()
        {
            //Range
            var veiculo = new Veiculo();
            veiculo.Nome = "Corolla";
            veiculo.Marca = "Toyota";
            veiculo.Ano = 2020;
            using var contexto = TestDBContexto.Create();
            var veiculoService = new VeiculoService(contexto);
            //Act
            veiculoService.Incluir(veiculo);
            var veiculoDoBanco = veiculoService.BuscarPorId(veiculo.Id);

            //Assert
            Assert.AreEqual(3, veiculoDoBanco.Id);
        }
        [TestMethod]
        public void BuscarPorIdInexistente()
        {
            //Range
            using var contexto = TestDBContexto.Create();
            var veiculoService = new VeiculoService(contexto);
            //Act
            var veiculoBuscado = veiculoService.BuscarPorId(999);
            //Assert
            Assert.IsNull(veiculoBuscado);
        }
        [TestMethod]
        public void BuscarTodos()
        {
            //Range
            using var contexto = TestDBContexto.Create();
            var veiculoService = new VeiculoService(contexto);
            //Act
            var listaVeiculos = veiculoService.Listar(1);
            //Assert
            Assert.IsNotNull(listaVeiculos, "A lista de administradores não pode ser nula");
            Assert.IsTrue(listaVeiculos.Count > 0, "A quantidade de administradores deve ser maior que 1 registro.");
        }
        [TestMethod]
        public void Atualizar()
        {
            //Range
            using var contexto = TestDBContexto.Create();
            var veiculoService = new VeiculoService(contexto);
            var veiculoDoBanco = veiculoService.BuscarPorId(1);
            veiculoDoBanco.Nome = "Onix 2.0";

            //Act
            veiculoService.Atualizar(veiculoDoBanco);
            var veiculoAtualizado = veiculoService.BuscarPorId(1);

            //Assets
            Assert.AreEqual(veiculoDoBanco.Nome, veiculoAtualizado.Nome);
        }
        [TestMethod]
        public void AtualizarObjetoInvalido()
        {
            //Range
            using var contexto = TestDBContexto.Create();
            var veiculoService = new VeiculoService(contexto);
            var veiculo = new Veiculo
            {
                Id = 999,
                Ano = 2005,
                Marca = "Chevrolet",
                Nome = "Onix"
            };
            //Act
            var ex = Assert.ThrowsException<KeyNotFoundException>
            (() => veiculoService.Atualizar(veiculo));
            //Assert
            Assert.AreEqual("Veiculo não encontrado.", ex.Message);
        }
        [TestMethod]
        public void AtualizarObjetoVazio()
        {
            //Range
            using var contexto = TestDBContexto.Create();
            var veiculoService = new VeiculoService(contexto);
            //Act
            var ex = Assert.ThrowsException<InvalidOperationException>
           (() => veiculoService.Atualizar(null));
            //Assert
            Assert.AreEqual("O veículo não pode ser nulo.", ex.Message);

        }
        [TestMethod]
        public void Deletar()
        {
            //Range
            using var contexto = TestDBContexto.Create();
            var veiculoService = new VeiculoService(contexto);
            var veiculo = veiculoService.BuscarPorId(2);
            //Act
            veiculoService.Apagar(veiculo);
            var veiculoDeletado = veiculoService.BuscarPorId(2);

            // Assert
            Assert.IsNull(veiculoDeletado);
        }
        [TestMethod]
        public void DeletarObjetoInvalido()
        {
            //Range
            using var contexto = TestDBContexto.Create();
            var veiculoService = new VeiculoService(contexto);
            var veiculo = veiculoService.BuscarPorId(1);
            veiculo.Id = 999;
            //Act
            var ex = Assert.ThrowsException<KeyNotFoundException>
            (() => veiculoService.Apagar(veiculo));
            //Assert
            Assert.AreEqual("Veiculo não encontrado.", ex.Message);
        }
        [TestMethod]
        public void DeletarObjetoVazio()
        {
            //Range
            using var contexto = TestDBContexto.Create();
            var veiculoService = new VeiculoService(contexto);
            //Act
            var ex = Assert.ThrowsException<InvalidOperationException>
            (() => veiculoService.Apagar(null));
            //Assert
            Assert.AreEqual("O veículo não pode ser nulo.", ex.Message);
        }
    }
}