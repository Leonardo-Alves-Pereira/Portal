using FluentValidation;
using Portal.Comunicacao.Requisicao;
using Portal.Exceptions.Resources;

namespace Portal.Application.UseCases.Tarefa.Registrar;

public class RegistrarTarefaValidator : AbstractValidator<RequisicaoTarefaJson>
{
    public RegistrarTarefaValidator()
    {
        RuleFor(x => x.Título).NotEmpty();
        RuleFor(x => x.Descrição).NotEmpty();
        //RuleForEach(x => x.Tarefas).ChildRules(Tarefa =>
        //{
        //    Tarefa.RuleFor(y => y.Nome).NotEmpty();
        //    Tarefa.RuleFor(y => y.Quantidade).NotEmpty();

        //});

    }
}
