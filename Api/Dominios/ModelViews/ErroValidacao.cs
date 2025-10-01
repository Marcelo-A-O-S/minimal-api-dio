using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Dominios.ModelViews
{
    public class ErroValidacao
    {
        public List<string> MensagemErro { get; set; }
        public bool Erro { get; set; }
    }
}