using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Portal.Application.Servicos.Token;
using Portal.Comunicacao.Resposta;
using Portal.Domain.Repositorio.Usuario;
using Portal.Exceptions.ExceptionBase;
using Portal.Exceptions.Resources;

namespace Portal.Api.Filtros;

public class UsuarioAutenticadoAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepositorio _repositorio;
    public UsuarioAutenticadoAttribute(TokenController tokenController, IUsuarioReadOnlyRepositorio repositorio)
    {
        _tokenController = tokenController;
        _repositorio = repositorio;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

        try
        {
            var token = TokenNaRequisicao(context);

            var email = _tokenController.RecuperarEmail(token);

            var usuario = await _repositorio.RecuperarPorEmail(email);

            if (usuario is null)
            {
                throw new PortalException(string.Empty);
            }
        }
        catch (SecurityTokenExpiredException)
        {
            TokenExpirado(context);
        }
        catch
        {
            UsuarioSemPermissão(context);
        }
    }
    private static string TokenNaRequisicao(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorization))
        {
            throw new PortalException(string.Empty);
        }

        return authorization["Bearer".Length..].Trim();
    }

    private static void TokenExpirado(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceErrorMessage.TOKEN_EXPIRADO));
    }
    private static void UsuarioSemPermissão(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceErrorMessage.USUARIO_SEM_PERMISSAO));
    }
}
