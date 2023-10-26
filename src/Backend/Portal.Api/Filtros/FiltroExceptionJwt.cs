using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Portal.Api.Filtros
{
    public class FiltroExceptionJwt : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is SecurityTokenException)
            {
                // Manipule a exceção do token JWT aqui, se desejar
                context.Result = new BadRequestObjectResult("Token JWT inválido");
                context.ExceptionHandled = true;
            }
            else if (context.Exception is ArgumentException)
            {
                // Manipule a exceção ArgumentException aqui, se desejar
                context.Result = new BadRequestObjectResult("Argumento inválido");
                context.ExceptionHandled = true;
            }
            // Adicione mais blocos 'else if' conforme necessário para lidar com outras exceções

            base.OnException(context);
        }
    }
}