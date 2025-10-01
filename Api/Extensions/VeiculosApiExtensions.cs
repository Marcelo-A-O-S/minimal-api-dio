using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.Entities;
using minimal_api.Dominios.Interfaces;
using minimal_api.Dominios.Validators;
namespace minimal_api.extensions
{
    public static class VeiculosApiExtensions
    {
        
        public static IEndpointRouteBuilder MapVeiculosApi(
            this IEndpointRouteBuilder app
        )
        {
            app.MapGet("/veiculos", ([FromQuery] int? pagina, IVeiculoService veiculoService) =>
            {
                var veiculos = veiculoService.Listar(pagina);
                return Results.Ok(veiculos);
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
            .WithTags("Veiculos")
            .WithDescription("Listar registros de veiculos.");
            app.MapGet("/veiculos/{veiculoId}", ([FromRoute] int veiculoId, IVeiculoService veiculoService) =>
            {
                var veiculo = veiculoService.BuscarPorId(veiculoId);
                if (veiculo == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(veiculo);
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
            .WithTags("Veiculos")
            .WithDescription("Buscar veiculo por Id.");
            app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoService veiculoService) =>
            {

                var validacao = VeiculoValidator.ValidarVeiculo(veiculoDTO);
                if (validacao.Erro)
                {
                    return Results.BadRequest(validacao.MensagemErro);
                }
                var veiculo = new Veiculo
                {
                    Ano = veiculoDTO.Ano,
                    Marca = veiculoDTO.Marca,
                    Nome = veiculoDTO.Nome
                };
                veiculoService.Incluir(veiculo);
                return Results.Created($"/veiculo/{veiculo.Id}", veiculo);

            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
            .WithTags("Veiculos")
            .WithDescription("Criar registro de veiculo.");
            app.MapPut("/veiculos/{veiculoId}", ([FromRoute] int veiculoId, VeiculoDTO veiculoDTO, IVeiculoService veiculoService) =>
            {
                var validacao = VeiculoValidator.ValidarVeiculo(veiculoDTO);
                if (validacao.Erro)
                {
                    return Results.BadRequest(validacao.MensagemErro);
                }
                var veiculo = veiculoService.BuscarPorId(veiculoId);
                if (veiculo == null) return Results.NotFound();
                veiculo.Nome = veiculoDTO.Nome;
                veiculo.Marca = veiculoDTO.Marca;
                veiculo.Ano = veiculoDTO.Ano;
                veiculoService.Atualizar(veiculo);
                return Results.Ok(veiculo);
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
            .WithTags("Veiculos")
            .WithDescription("Buscar registro de veiculo por Id.");
            app.MapDelete("/veiculos/{veiculoId}", ([FromRoute] int veiculoId, IVeiculoService veiculoService) =>
            {
                var veiculo = veiculoService.BuscarPorId(veiculoId);
                if (veiculo == null) return Results.NotFound();
                veiculoService.Apagar(veiculo);
                return Results.Ok();
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
            .WithTags("Veiculos")
            .WithDescription("Deletar registro de veiculo por Id.");
            return app;
        }
    }
}