using Portal.Comunicacao.Resposta;

namespace Portal.Application.UseCases.Tarefa.Listar;

public interface IListarTarefaIdUseCase
{
    Task<RespostaTarefaJson> Executar(int ?id);
}
