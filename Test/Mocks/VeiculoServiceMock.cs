using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominios.Entities;
using minimal_api.Dominios.Interfaces;

namespace Test.Mocks
{
    public class VeiculoServiceMock : IVeiculoService
    {
        public static List<Veiculo> veiculos = new List<Veiculo>()
        {
            new Veiculo{
                Id = 1,
                Ano = 2005,
                Marca = "Chevrolet",
                Nome = "Onix"
            },
            new Veiculo{
                Id = 2,
                Ano = 2018,
                Marca = "Volkswagen",
                Nome = "Golf"
            },
            new Veiculo{
                Id = 3,
                Ano = 2020,
                Marca = "Toyota",
                Nome = "Corolla"
            }
        };
        public void Apagar(Veiculo veiculo)
        {
            veiculos.Remove(veiculo);
        }

        public void Atualizar(Veiculo veiculo)
        {
            var index = veiculos.FindIndex(v => v.Id == veiculo.Id);
            if (index != -1)
            {
                veiculos[index] = veiculo;
            }
        }

        public Veiculo BuscarPorId(int id)
        {
            return veiculos.Find(v => v.Id == id);
        }

        public void Incluir(Veiculo veiculo)
        {
            veiculo.Id = veiculos.Count() + 1;
            veiculos.Add(veiculo);
        }

        public List<Veiculo> Listar(int? pagina = 1, string nome = null, string marca = null)
        {
            return veiculos.ToList();
        }
    }
}