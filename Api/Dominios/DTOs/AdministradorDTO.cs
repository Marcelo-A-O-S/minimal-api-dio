using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominios.Enums;

namespace minimal_api.Dominios.DTOs
{
    public class AdministradorDTO
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public Perfil? Perfil { get; set; }
    }
}