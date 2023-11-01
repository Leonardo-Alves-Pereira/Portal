using FluentValidation;
using Portal.Comunicacao.Requisicao;
using Portal.Exceptions.Resources;

namespace Portal.Application.UseCases.Tarefa.Registrar;

public class RegistrarTarefaValidator : AbstractValidator<RequisicaoTarefaJson>
{
    public RegistrarTarefaValidator()
    {
        RuleFor(x => x.Titulo).NotEmpty();
        RuleFor(x => x.Descricao).NotEmpty();
        //RuleForEach(x => x.Tarefas).ChildRules(Tarefa =>
        //{
        //    Tarefa.RuleFor(y => y.Nome).NotEmpty();
        //    Tarefa.RuleFor(y => y.Quantidade).NotEmpty();

        //});

    }
}
