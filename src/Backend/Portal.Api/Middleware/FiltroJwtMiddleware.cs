using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Portal.Comunicacao.Resposta;
using Portal.Exceptions.Resources;

namespace Portal.Api.Middleware;

public class FiltroJwtMiddleware
{
    private readonly RequestDelegate _next;

    public FiltroJwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (SecurityTokenException)
        {
            await ErroDesconhecido(context);
        }
        catch (ArgumentException)
        {
            await ErroDesconhecido(context);
        }
            
    }

    private async Task ErroDesconhecido(HttpContext context)
    {
        var respostaJson = new ObjectResult(new RespostaErroJson(ResourceErrorMessage.ERRO_DESCONHECIDO));
        var respostaErro = JsonConvert.SerializeObject(respostaJson.Value);
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(respostaErro);
    }
}
