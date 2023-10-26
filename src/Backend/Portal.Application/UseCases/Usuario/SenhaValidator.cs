using FluentValidation;
using Portal.Exceptions.Resources;

namespace Portal.Application.UseCases.Usuario;

public class SenhaValidator : AbstractValidator<string>
{
    public SenhaValidator()
    {
        RuleFor(senha => senha).NotEmpty().WithMessage(ResourceErrorMessage.SENHA_VAZIA);

        When(senha => !string.IsNullOrWhiteSpace(senha), () =>
        {
            RuleFor(senha => senha.Length).GreaterThanOrEqualTo(8).WithMessage(ResourceErrorMessage.SENHA_INVALIDA_QTD);
        });
    }
}
