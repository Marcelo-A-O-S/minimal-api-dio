using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.Entities;
using minimal_api.Dominios.Interfaces;
using minimal_api.Infraestrutura.DB;
namespace minimal_api.Dominios.Services
{
    public class AdministradorService : IAdministradorService
    {
        private readonly DBContexto contexto;
        public AdministradorService(DBContexto _contexto)
        {
            this.contexto = _contexto;
        }

        public void Apagar(Administrador administrador)
        {
            if (administrador == null)
                throw new InvalidOperationException("O administrador não pode ser nulo.");
            if (this.BuscarPorId(administrador.Id) == null)
                throw new KeyNotFoundException("Administrador não encontrado para deletar.");
            this.contexto.Administradores.Remove(administrador);
            this.contexto.SaveChanges();
        }

        public void Atualizar(Administrador administrador)
        {
            if (administrador == null)
                throw new InvalidOperationException("O administrador não pode ser nulo.");
            if (this.BuscarPorId(administrador.Id) == null)
                throw new KeyNotFoundException("Administrador não encontrado para atualização.");
            this.contexto.Administradores.Update(administrador);
            this.contexto.SaveChanges();
        }

        public Administrador BuscarPorEmail(string email)
        {
            var adm = this.contexto.Administradores.Where(a => a.Email == email).FirstOrDefault();
            return adm;
        }

        public Administrador BuscarPorId(int Id)
        {
            var adm = this.contexto.Administradores.Where(a => a.Id == Id).FirstOrDefault();
            return adm;
        }

        public Administrador Incluir(Administrador administrador)
        {
            if (administrador == null)
                throw new InvalidOperationException("O administrador não pode ser nulo.");
            if (contexto.Administradores.Any(a => a.Email == administrador.Email))
                throw new InvalidOperationException("Já existe administrador com esse e-mail.");
            this.contexto.Administradores.Add(administrador);
            this.contexto.SaveChanges();
            return administrador;
        }

        public List<Administrador> Listar(int? pagina)
        {
            var query = this.contexto.Administradores.AsQueryable();
            int itensPorPagina = 10;
            if (pagina != null)
            {
                query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
            }
            return query.ToList();
        }

        public Administrador Login(LoginDTO loginDTO)
        {
            var adm = contexto.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
            return adm;
        }
    }
}