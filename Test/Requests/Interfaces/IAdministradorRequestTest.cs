using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Requests.Interfaces
{
    public interface IAdministradorRequestTest : IRequestTest
    {
        Task CriarPorEmailDuplicado();
    }
}