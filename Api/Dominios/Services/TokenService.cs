using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using minimal_api.Dominios.Entities;
using minimal_api.Dominios.Interfaces;

namespace minimal_api.Dominios.Services
{
    public class TokenService : ITokenService
    {
        private readonly string Key;
        public TokenService(IConfiguration configuration)
        {
            Key = configuration["Jwt"] ?? "123456";
        }
        public string GerarToken(Administrador administrador)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim("Email", administrador.Email),
            new Claim("Perfil", administrador.Perfil),
            new Claim(ClaimTypes.Role, administrador.Perfil)
        };

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}