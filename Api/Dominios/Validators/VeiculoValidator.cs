using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.ModelViews;

namespace minimal_api.Dominios.Validators
{
    public class VeiculoValidator
    {
        public static ErroValidacao ValidarVeiculo(VeiculoDTO veiculoDTO)
        {
            var validacao = new ErroValidacao
            {
                MensagemErro = new List<string>()
            };
            if (string.IsNullOrEmpty(veiculoDTO.Nome))
            {
                validacao.MensagemErro.Add("O Campo Nome não pode ser vazio.");
            }
            if (string.IsNullOrEmpty(veiculoDTO.Marca))
            {
                validacao.MensagemErro.Add("O Campo Marca não pode ser vazio.");
            }
            if (veiculoDTO.Ano == 0 || veiculoDTO.Ano < 1885)
            {
                validacao.MensagemErro.Add("O Campo Ano não pode ser zerado ou menor do que o ano de criação do primeiro automovel");
            }
            if (validacao.MensagemErro.Count > 0)
            {
                validacao.Erro = true;
            }
            return validacao;
        }
    }
}