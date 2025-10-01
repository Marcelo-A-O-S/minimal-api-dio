using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Dominios.ModelViews
{
    public struct Home
    {
        public string Mensagem { get => "Bem vindo a minha API"; }
        public string Doc { get => "/swagger"; }
    }
}