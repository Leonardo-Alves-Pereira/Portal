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
        if (context.Exception is ErroDeValidacaoException)
            TratarErroDeValidacao(context);
        else if (context.Exception is LoginInvalidoException)
            LoginException(context);
        else if (context.Exception is ErroCategoriaException)
            ErroCategoriaException(context);
        else if (context.Exception is ErroTarefaException)
            ErroTarefaException(context);
    }

    private static void TratarErroDeValidacao(ExceptionContext context)
    {
        var erroDeValidacao = context.Exception as ErroDeValidacaoException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new RespostaErroJson(erroDeValidacao.MensagensDeErro));

    }

    private static void LoginException(ExceptionContext context)
    {
        var erroDeLogin = context.Exception as LoginInvalidoException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(new RespostaErroJson(erroDeLogin.Message));
    }

    private static void ErroCategoriaException(ExceptionContext context)
    {
        var ErroDeCategoria = context.Exception as ErroCategoriaException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new RespostaErroJson(ErroDeCategoria.MensagensDeErro));
    }

    private static void ErroTarefaException(ExceptionContext context)
    {
        var ErroDeCategoria = context.Exception as ErroTarefaException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new RespostaErroJson(ErroDeCategoria.MensagensDeErro));
    }

    private static void ErroDesconhecido(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new RespostaErroJson(ResourceErrorMessage.ERRO_DESCONHECIDO));
    }
}
