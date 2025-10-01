using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominios.Entities;

namespace minimal_api.Dominios.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(Administrador administrador);
    }
}