using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.Entities;
using minimal_api.Dominios.Enums;
using minimal_api.Dominios.Interfaces;
using minimal_api.Dominios.ModelViews;
using minimal_api.Dominios.Validators;
namespace minimal_api.Extensions
{
    public static class AdministradorApiExtensions
    {
        public static IEndpointRouteBuilder MapAdministradorApi(
            this IEndpointRouteBuilder app
        )
        {
            app.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministradorService administradorService, ITokenService tokenService) =>
            {
                var administrador = administradorService.Login(loginDTO);
                if (administrador != null)
                {
                    if (administrador.Senha == loginDTO.Senha)
                    {
                        string token = tokenService.GerarToken(administrador);
                        return Results.Ok(new AdministradorLogado
                        {
                            Email = administrador.Email,
                            Perfil = administrador.Perfil,
                            Token = token
                        });
                    }
                    else
                    {
                        return Results.Unauthorized();
                    }
                }
                else
                {
                    return Results.Unauthorized();
                }
            }).AllowAnonymous().WithTags("Administradores").WithDescription("Login de administrador.");
            app.MapPost("/administradores", ([FromBody] AdministradorDTO administradorDTO, IAdministradorService administradorService) =>
            {
                try
                {
                    var validacao = AdministradorValidator.ValidarAdministrador(administradorDTO);
                    if (validacao.Erro)
                    {
                        return Results.BadRequest(validacao.MensagemErro);
                    }
                    var adm = new Administrador
                    {
                        Email = administradorDTO.Email,
                        Perfil = administradorDTO.Perfil.ToString() ?? Perfil.Editor.ToString(),
                        Senha = administradorDTO.Senha
                    };
                    administradorService.Incluir(adm);
                    return Results.Created($"/administradores/{adm.Id}", new AdministradorModelView
                    {
                        Email = adm.Email,
                        Id = adm.Id,
                        Perfil = adm.Perfil
                    });
                }
                catch (InvalidOperationException ex) when (ex.Message.Contains("e-mail"))
                {
                    return Results.Conflict(new { message = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }).RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
            .WithTags("Administradores")
            .WithDescription("Criar registro de administrador.");
            app.MapPut("/Administradores/{administradorId}", ([FromRoute] int administradorId, AdministradorDTO administradorDTO, IAdministradorService administradorService) =>
            {
                var validacao = AdministradorValidator.ValidarAdministrador(administradorDTO);
                if (validacao.Erro)
                {
                    return Results.BadRequest(validacao.MensagemErro);
                }
                var administrador = administradorService.BuscarPorId(administradorId);
                if (administrador == null) return Results.NotFound();
                administrador.Email = administradorDTO.Email;
                administrador.Perfil = administradorDTO.Perfil.ToString() ?? Perfil.Editor.ToString();
                administrador.Senha = administradorDTO.Senha;
                administradorService.Atualizar(administrador);
                return Results.Ok(administrador);
            }).RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
            .WithTags("Administradores")
            .WithDescription("Atualizar registro de administrador.");
            app.MapGet("/Administradores", ([FromQuery] int? pagina, IAdministradorService administradorService) =>
            {
                var admViews = new List<AdministradorModelView>();
                var adms = administradorService.Listar(pagina);
                foreach (var adm in adms)
                {
                    admViews.Add(new AdministradorModelView
                    {
                        Id = adm.Id,
                        Email = adm.Email,
                        Perfil = adm.Perfil
                    });
                }
                return Results.Ok(admViews);
            }).RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
            .WithTags("Administradores")
            .WithDescription("Listar registros dos administradores.");
            app.MapGet("/Administradores/{administradorId}", ([FromRoute] int administradorId, IAdministradorService administradorService) =>
            {
                var adm = administradorService.BuscarPorId(administradorId);
                if (adm == null) return Results.NotFound();
                var admView = new AdministradorModelView
                {
                    Email = adm.Email,
                    Id = adm.Id,
                    Perfil = adm.Perfil
                };
                return Results.Ok(admView);
            }).RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
            .WithTags("Administradores")
            .WithDescription("Buscar registro de administrador por Id");
            app.MapDelete("/Administradores/{administradorId}", (int administradorId, IAdministradorService administradorService) =>
            {
                var adm = administradorService.BuscarPorId(administradorId);
                if (adm == null) return Results.NotFound();
                administradorService.Apagar(adm);
                return Results.Ok("Administrador deletado com sucesso!");
            }).RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
            .WithTags("Administradores")
            .WithDescription("Deletar registro de administrador por Id.");
            return app;
        }
    }
}