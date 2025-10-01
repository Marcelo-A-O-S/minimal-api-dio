using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.ModelViews;

namespace minimal_api.Dominios.Validators
{
    public static class AdministradorValidator
    {
        public static ErroValidacao ValidarAdministrador(AdministradorDTO administradorDTO)
        {
            var validacao = new ErroValidacao
            {
                MensagemErro = new List<string>()
            };
            if (string.IsNullOrEmpty(administradorDTO.Email))
            {
                validacao.MensagemErro.Add("O Campo Email não pode ser vazio.");
            }
            if (administradorDTO.Perfil == null)
            {
                validacao.MensagemErro.Add("O Campo Perfil não pode ser vazio.");
            }
            if (string.IsNullOrEmpty(administradorDTO.Senha))
            {
                validacao.MensagemErro.Add("O Campo Senha não pode ser vazio.");
            }
            if (validacao.MensagemErro.Count > 0)
            {
                validacao.Erro = true;
            }
            return validacao;

        }
    }
}