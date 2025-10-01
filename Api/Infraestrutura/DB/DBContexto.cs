using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominios.Entities;

namespace minimal_api.Infraestrutura.DB
{
    public class DBContexto : DbContext
    {
        private readonly IConfiguration configurationSettings;

        public DBContexto(IConfiguration _configuration)
        {
            this.configurationSettings = _configuration;
        }

        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>().HasData(
                new Administrador
                {
                    Id = 1,
                    Email = "administrador@teste.com",
                    Senha = "123456",
                    Perfil = "Adm"
                },
                new Administrador
                {
                    Id = 2,
                    Email = "editor@teste.com",
                    Senha = "123456",
                    Perfil = "Editor"
                }
            );
            modelBuilder.Entity<Veiculo>().HasData(
                new Veiculo
                {
                    Id = 1,
                    Ano = 2005,
                    Marca = "Chevrolet",
                    Nome = "Onix"
                },
                new Veiculo
                {
                    Id = 2,
                    Ano = 2018,
                    Marca = "Volkswagen",
                    Nome = "Golf"
                }
            );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var stringConnection = this.configurationSettings.GetConnectionString("mysql").ToString();
            if (!string.IsNullOrEmpty(stringConnection))
            {
                optionsBuilder.UseMySql(stringConnection, ServerVersion.AutoDetect(stringConnection));
            }
        }
    }
}