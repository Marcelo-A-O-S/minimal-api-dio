using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Dominios.ModelViews
{
    public class AdministradorLogado
    {
        public string Email { get; set; }
        public string Perfil { get; set; }
        public string Token { get; set; }
    }
}