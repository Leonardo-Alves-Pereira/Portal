using FluentValidation;
using Portal.Comunicacao.Requisicao;
using Portal.Exceptions.Resources;
using System.Text.RegularExpressions;

namespace Portal.Application.UseCases.Usuario.Registrar;

public class LoginValidator : AbstractValidator<RequisicaoUsuarioJson>
{
    public LoginValidator()
    {
        RuleFor(c => c.Email).NotEmpty().WithMessage(ResourceErrorMessage.LOGIN_INVALIDO).OverridePropertyName("loginSenhaInvalido");
    }
}
