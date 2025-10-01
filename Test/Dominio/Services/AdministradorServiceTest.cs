using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using minimal_api.Dominios.Entities;
using minimal_api.Dominios.Services;
using Test.Infraestrutura;
using Test.Dominio.Services.Interfaces;

namespace Test.Dominio.Services
{
    [TestClass]
    public class AdministradorServiceTest : ITestServices
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
            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";
            using var contexto = TestDBContexto.Create();
            var administradorService = new AdministradorService(contexto);
            var quantidadeTotal = administradorService.Listar(1).Count();
            //Act
            administradorService.Incluir(adm);
            //Assert
            Assert.AreEqual(quantidadeTotal + 1, administradorService.Listar(1).Count());
        }
        [TestMethod]
        public void SalvarObjetoInvalido()
        {
            //Range
            var adm = new Administrador();
            adm.Email = "administrador@teste.com";
            adm.Senha = "123456";
            adm.Perfil = "Adm";
            using var contexto = TestDBContexto.Create();
            var administradorService = new AdministradorService(contexto);
            //Act
            var ex = Assert.ThrowsException<InvalidOperationException>(() =>
            {
                administradorService.Incluir(adm);
            });
            //Assert
            Assert.AreEqual("Já existe administrador com esse e-mail.", ex.Message);
        }
        [TestMethod]
        public void SalvarObjetoVazio()
        {
            //Range
            using var contexto = TestDBContexto.Create();
            var administradorService = new AdministradorService(contexto);
            //Act
            var ex = Assert.ThrowsException<InvalidOperationException>(() =>
            {
                administradorService.Incluir(null);
            });
            //Assert
            Assert.AreEqual("O administrador não pode ser nulo.", ex.Message);
        }
        [TestMethod]
        public void BuscarPorId()
        {
            //Range
            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";
            using var contexto = TestDBContexto.Create();
            var administradorService = new AdministradorService(contexto);
            //Act
            administradorService.Incluir(adm);
            var admDoBanco = administradorService.BuscarPorId(adm.Id);
            //Assert
            Assert.AreEqual(3, admDoBanco.Id);
        }
        [TestMethod]
        public void BuscarPorIdInexistente()
        {
            //Range
            using var contexto = TestDBContexto.Create();
            var administradorService = new AdministradorService(contexto);
            //Act
            var admDoBanco = administradorService.BuscarPorId(10);
            //Assert
            Assert.IsNull(admDoBanco);
        }
        [TestMethod]
        public void BuscarTodos()
        {
            //Range
            using var contexto = TestDBContexto.Create();
            var administradorService = new AdministradorService(contexto);
            //Act
            var listaAdms = administradorService.Listar(1);
            //Assert
            Assert.IsNotNull(listaAdms, "A lista de administradores não pode ser nula");
            Assert.IsTrue(listaAdms.Count > 0, "A quantidade de administradores deve ser maior que 1 registro.");

        }
        [TestMethod]
        public void Atualizar()
        {
            // Arrange
            using var contexto = TestDBContexto.Create();
            var service = new AdministradorService(contexto);
            var adm = service.BuscarPorId(1);
            adm.Email = "novoemail@teste.com";

            // Act
            service.Atualizar(adm);
            var atualizado = service.BuscarPorId(1);

            // Assert
            Assert.AreEqual(adm.Email, atualizado.Email);
        }
        [TestMethod]
        public void AtualizarObjetoInvalido()
        {
            // Arrange
            using var contexto = TestDBContexto.Create();
            var service = new AdministradorService(contexto);
            var admInexistente = new Administrador { Id = 999, Email = "x@teste.com", Perfil = "Adm", Senha = "123" };

            // Act & Assert
            var ex = Assert.ThrowsException<KeyNotFoundException>
            (() => service.Atualizar(admInexistente));
            Assert.AreEqual("Administrador não encontrado para atualização.", ex.Message);
        }
        [TestMethod]
        public void AtualizarObjetoVazio()
        {
            // Arrange
            using var contexto = TestDBContexto.Create();
            var service = new AdministradorService(contexto);

            // Act 
            var ex = Assert.ThrowsException<InvalidOperationException>
            (() => service.Atualizar(null));
            //Assert
            Assert.AreEqual("O administrador não pode ser nulo.", ex.Message);
        }
        [TestMethod]
        public void Deletar()
        {
            //Arrange
            using var contexto = TestDBContexto.Create();
            var administradorService = new AdministradorService(contexto);
            var adm = administradorService.BuscarPorId(2);
            //Act
            administradorService.Apagar(adm);
            var admDeletado = administradorService.BuscarPorId(2);

            // Assert
            Assert.IsNull(admDeletado);
        }
        [TestMethod]
        public void DeletarObjetoInvalido()
        {
             // Arrange
            using var contexto = TestDBContexto.Create();
            var administradorService = new AdministradorService(contexto);
            var admInexistente = new Administrador { Id = 999, Email = "x@teste.com", Perfil = "Adm", Senha = "123" };

            // Act & Assert
            var ex = Assert.ThrowsException<KeyNotFoundException>
            (() => administradorService.Apagar(admInexistente));
            Assert.AreEqual("Administrador não encontrado para deletar.", ex.Message);
        }
        [TestMethod]
        public void DeletarObjetoVazio()
        {
            // Arrange
            using var contexto = TestDBContexto.Create();
            var service = new AdministradorService(contexto);

            // Act 
            var ex = Assert.ThrowsException<InvalidOperationException>
            (() => service.Apagar(null));
            //Assert
            Assert.AreEqual("O administrador não pode ser nulo.", ex.Message);
        }
    }
}