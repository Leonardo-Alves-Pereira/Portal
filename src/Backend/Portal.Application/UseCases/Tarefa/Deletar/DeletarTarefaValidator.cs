using FluentValidation;
using Portal.Comunicacao.Requisicao;
using Portal.Exceptions.Resources;

namespace Portal.Application.UseCases.Tarefa.Deletar;

public class DeletarTarefaValidator : AbstractValidator<RequisicaoTarefaJson>
{
    public DeletarTarefaValidator()
    {
        RuleFor(x => x.Titulo).NotEmpty().WithMessage(ResourceErrorMessage.USUARIO_VAZIO)
                            .NotNull().WithMessage("Não pode ser nulo");
        RuleFor(x => x.Descricao).NotEmpty().WithMessage("Descrição não pode está vazia")
                                 .NotNull().WithMessage("Não pode ser nulo");
        RuleFor(x => x.Id).NotEqual(0).WithMessage(ResourceErrorMessage.OBJETO_NAO_ENCONTRADO);
    }
}
