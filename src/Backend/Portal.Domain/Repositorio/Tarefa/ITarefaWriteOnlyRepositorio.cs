namespace Portal.Domain.Repositorio.Tarefa;

public interface ITarefaWriteOnlyRepositorio
{
    Task Registrar(Entidade.Tarefa Tarefa);
}
