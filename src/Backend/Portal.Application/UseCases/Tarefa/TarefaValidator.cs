using FluentValidation;
using Portal.Exceptions.Resources;

namespace Portal.Application.UseCases.Tarefa;

public class TarefaValidator : AbstractValidator<int>
{
    public TarefaValidator()
    {
        RuleFor(id => id).NotEqual(0).WithMessage(ResourceErrorMessage.OBJETO_NAO_ENCONTRADO)
                         .NotNull().WithMessage(ResourceErrorMessage.OBJETO_NAO_ENCONTRADO);

    }
}