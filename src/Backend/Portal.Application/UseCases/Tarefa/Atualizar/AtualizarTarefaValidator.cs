using FluentValidation;
using Portal.Comunicacao.Requisicao;
using Portal.Exceptions.Resources;

namespace Portal.Application.UseCases.Tarefa.Atualizar;

public class AtualizarTarefaValidator : AbstractValidator<RequisicaoTarefaJson>
{
    public AtualizarTarefaValidator()
    {
        RuleFor(x => x.Título).NotEmpty().WithMessage(ResourceErrorMessage.USUARIO_VAZIO)
                            .NotNull().WithMessage("Não pode ser nulo");
        RuleFor(x => x.Descrição).NotEmpty().WithMessage("Descrição não pode está vazia")
                                 .NotNull().WithMessage("Não pode ser nulo");
        RuleFor(x => x.Id).NotEqual(0).WithMessage(ResourceErrorMessage.OBJETO_NAO_ENCONTRADO);
        //RuleForEach(x => x.Tarefas).ChildRules(Tarefa =>
        //{
        //    Tarefa.RuleFor(y => y.Nome).NotEmpty();
        //    Tarefa.RuleFor(y => y.Quantidade).NotEmpty();

        //});

    }
}
