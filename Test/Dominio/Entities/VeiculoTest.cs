using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using minimal_api.Dominios.Entities;
namespace Test.Dominio.Entities
{
    [TestClass]
    public class VeiculoTest
    {
        [TestMethod("Teste de veiculo")]
        public void TestarGetSetPropriedades()
        {
            var veiculo = new Veiculo();

            veiculo.Id = 1;
            veiculo.Nome = "Onix";
            veiculo.Marca = "Chevrolet";
            veiculo.Ano = 2005;

            Assert.AreEqual(1, veiculo.Id);
            Assert.AreEqual("Onix", veiculo.Nome);
            Assert.AreEqual("Chevrolet", veiculo.Marca);
            Assert.AreEqual(2005, veiculo.Ano);
        }
    }
}