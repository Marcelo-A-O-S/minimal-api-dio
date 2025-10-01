using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominios.Enums;

namespace minimal_api.Dominios.ModelViews
{
    public class AdministradorModelView
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public String Perfil { get; set; }
    }
}