using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Portal.Application.Servicos.Token;
using Portal.Comunicacao.Resposta;
using Portal.Domain.Repositorio.Usuario;
using Portal.Exceptions.Resources;

namespace Portal.Api.Filtros;

public class LoginAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepositorio _repositorio;
    public LoginAttribute(TokenController tokenController, IUsuarioReadOnlyRepositorio repositorio)
    {
        _tokenController = tokenController;
        _repositorio = repositorio;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var token = TokenNaRequisicao(context);

        if (!string.IsNullOrEmpty(token))
        {
            var email = _tokenController.RecuperarEmail(token);
            var usuario = await _repositorio.RecuperarPorEmail(email);

            if (usuario != null)
                context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceErrorMessage.USUARIO_LOGADO));
        }
    }
    private static string TokenNaRequisicao(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (authorization.Length <= 25)
            authorization = "Bearer";

        return authorization["Bearer".Length..].Trim();
    }
}
