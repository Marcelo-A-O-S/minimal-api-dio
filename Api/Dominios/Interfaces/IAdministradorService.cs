using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.Entities;

namespace minimal_api.Dominios.Interfaces
{
    public interface IAdministradorService
    {
        Administrador Login(LoginDTO loginDTO);
        Administrador BuscarPorId(int Id);
        Administrador BuscarPorEmail(string email);
        List<Administrador> Listar(int? pagina);
        Administrador Incluir(Administrador administrador);
        void Atualizar(Administrador administrador);
        void Apagar(Administrador administrador);

    }
}