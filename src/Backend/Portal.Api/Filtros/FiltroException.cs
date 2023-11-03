using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Portal.Comunicacao.Resposta;
using Portal.Exceptions.ExceptionBase;
using Portal.Exceptions.Resources;
using System.Net;

namespace Portal.Api.Filtros;

public class FiltroException : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is PortalException)
            TratarException(context);
        else if (context.Exception is ArgumentException)
            ErroDesconhecido(context);
        else
            ErroDesconhecido(context);
    }

    private void TratarException(ExceptionContext context)
    {
        if ( context.Exception is ErroGenericoException)
            TratarErroGenerico(context);
    }

    private static void TratarErroGenerico(ExceptionContext context)
    {
        var erroDeValidacao = context.Exception as ErroGenericoException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(erroDeValidacao.MensagensDeErro);

    }
    private static void ErroDesconhecido(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new RespostaErroJson(ResourceErrorMessage.ERRO_DESCONHECIDO));
    }
}
