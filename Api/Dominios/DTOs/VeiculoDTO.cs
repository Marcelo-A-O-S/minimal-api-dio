using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Dominios.DTOs
{
    public class VeiculoDTO
    {
        public string Nome { get; set; }
        public string Marca { get; set; }
        public int Ano { get; set; }
    }
}