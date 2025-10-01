using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Infraestrutura.DB;

namespace Test.Infraestrutura
{
    public class TestDBContexto : DBContexto
    {

        public TestDBContexto(IConfiguration _configuration) : base(_configuration)
        {
        }

        public static TestDBContexto Create()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            return new TestDBContexto(configuration);
        }
    }
}