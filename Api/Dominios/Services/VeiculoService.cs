using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominios.Entities;
using minimal_api.Dominios.Interfaces;
using minimal_api.Infraestrutura.DB;

namespace minimal_api.Dominios.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly DBContexto contexto;

        public VeiculoService(DBContexto _contexto)
        {
            this.contexto = _contexto;
        }
        public void Apagar(Veiculo veiculo)
        {
            if (veiculo == null)
                throw new InvalidOperationException("O veículo não pode ser nulo.");
            if (this.BuscarPorId(veiculo.Id) == null)
                throw new KeyNotFoundException("Veiculo não encontrado.");
            this.contexto.Veiculos.Remove(veiculo);
            this.contexto.SaveChanges();
        }

        public void Atualizar(Veiculo veiculo)
        {
            if (veiculo == null)
                throw new InvalidOperationException("O veículo não pode ser nulo.");
            if (this.BuscarPorId(veiculo.Id) == null)
                throw new KeyNotFoundException("Veiculo não encontrado.");
            this.contexto.Veiculos.Update(veiculo);
            this.contexto.SaveChanges();
        }

        public Veiculo BuscarPorId(int id)
        {
            return this.contexto.Veiculos.Where(v => v.Id == id).FirstOrDefault();
        }

        public void Incluir(Veiculo veiculo)
        {
            if (veiculo == null)
                throw new InvalidOperationException("O veículo não pode ser nulo.");
            if (contexto.Veiculos.Any(v => v.Nome == veiculo.Nome))
                throw new InvalidOperationException("Já existe veículo com esse nome.");
            this.contexto.Veiculos.Add(veiculo);
            this.contexto.SaveChanges();
        }

        public List<Veiculo> Listar(int? pagina = 1, string nome = null, string marca = null)
        {
            var query = this.contexto.Veiculos.AsQueryable();
            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome.ToLower()}%"));
            }
            int itensPorPagina = 10;
            if (pagina != null)
            {
                query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
            }

            return query.ToList();
        }
    }
}