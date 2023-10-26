using FluentValidation;
using Portal.Comunicacao.Requisicao;
using Portal.Exceptions.Resources;
using System.Text.RegularExpressions;

namespace Portal.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioValidator : AbstractValidator<RequisicaoRegistrarUsuarioJson>
{
    public RegistrarUsuarioValidator()
    {
        RuleFor(c => c.Nome).NotEmpty().WithMessage(ResourceErrorMessage.USUARIO_VAZIO);
        RuleFor(c => c.Email).NotEmpty().WithMessage(ResourceErrorMessage.EMAIL_VAZIO);
        RuleFor(c => c.Telefone).NotEmpty().WithMessage(ResourceErrorMessage.TELEFONE_VAZIO);
        RuleFor(c => c.Senha).SetValidator(new SenhaValidator());
        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ResourceErrorMessage.EMAIL_INVALIDO);
        });
       
        When(c => !string.IsNullOrWhiteSpace(c.Telefone), () =>
        {
            RuleFor(c => c.Telefone).Custom((telefone, contexto) =>
            {
                string padraoTelefone = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                var eValido = Regex.IsMatch(telefone, padraoTelefone);
                if (!eValido)
                {
                    contexto.AddFailure(new FluentValidation
                            .Results.ValidationFailure(nameof(telefone), ResourceErrorMessage.TELEFONE_INVALIDO));
                }
            });
        });



    }
}
