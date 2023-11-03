using FluentValidation;
using Portal.Comunicacao.Requisicao;
using Portal.Exceptions.Resources;

namespace Portal.Application.UseCases.Tarefa.Deletar;

public class DeletarTarefaValidator : AbstractValidator<RequisicaoTarefaJson>
{
    public DeletarTarefaValidator()
    {
        RuleFor(x => x.Id).NotEqual(0).WithMessage(ResourceErrorMessage.OBJETO_NAO_ENCONTRADO);
    }
}
