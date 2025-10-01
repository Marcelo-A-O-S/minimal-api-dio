using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominios.Entities;

namespace minimal_api.Dominios.Interfaces
{
    public interface IVeiculoService
    {
        List<Veiculo> Listar(int? pagina = 1, string nome = null, string marca = null);
        Veiculo BuscarPorId(int id);
        void Incluir(Veiculo veiculo);
        void Atualizar(Veiculo veiculo);
        void Apagar(Veiculo veiculo);
    }
}