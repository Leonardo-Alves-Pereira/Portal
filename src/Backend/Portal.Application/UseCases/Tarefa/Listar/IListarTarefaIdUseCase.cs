namespace Portal.Application.UseCases.Tarefa.Listar;

public interface IListarTarefaIdUseCase
{
    Task<Domain.Entidade.Tarefa> Executar(int ?id);
}
