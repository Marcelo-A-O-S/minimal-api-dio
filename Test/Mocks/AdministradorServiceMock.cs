using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.Entities;
using minimal_api.Dominios.Interfaces;

namespace Test.Mocks
{
    public class AdministradorServiceMock : IAdministradorService
    {
        private static List<Administrador> administradores = new List<Administrador>(){
            new Administrador{
                Id = 1,
                Email = "adm@teste.com",
                Senha = "123456",
                Perfil = "Adm"
            },
            new Administrador{
                Id = 2,
                Email = "editor@teste.com",
                Senha = "123456",
                Perfil = "Editor"
            }
        };
        public void Apagar(Administrador administrador)
        {
            administradores.Remove(administrador);
        }

        public void Atualizar(Administrador administrador)
        {
            if (administrador == null || this.BuscarPorId(administrador.Id) == null)
                throw new KeyNotFoundException("Administrador não encontrado para atualização.");
            var index = administradores.FindIndex(v => v.Id == administrador.Id);
            if (index != -1)
            {
                administradores[index] = administrador;
            }
        }

        public Administrador BuscarPorEmail(string email)
        {
            return administradores.Find(a => a.Email == email);
        }

        public Administrador BuscarPorId(int Id)
        {
            return administradores.Find(a => a.Id == Id);
        }

        public Administrador Incluir(Administrador administrador)
        {
            if (administrador == null)
                throw new InvalidOperationException("O administrador não pode ser");
            if (administradores.Find(a => a.Email == administrador.Email) != null)
                throw new InvalidOperationException("Já existe administrador com esse e-mail.");
            administrador.Id = administradores.Count() + 1;
            administradores.Add(administrador);
            return administrador;
        }

        public List<Administrador> Listar(int? pagina)
        {
            return administradores.ToList();
        }

        public Administrador Login(LoginDTO loginDTO)
        {
            return administradores.Find(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha);
        }
    }
}