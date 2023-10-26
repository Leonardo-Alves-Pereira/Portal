using Portal.Comunicacao.Resposta;

namespace Portal.Application.UseCases.Tarefa.Listar;

public interface IListarTarefaUseCase
{
    Task<IList<RespostaTarefaJson>> Executar();
}
