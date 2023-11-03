using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portal.Domain.Repositorio.Tarefa;

public interface ITarefaReadOnlyRepositorio
{
    Task<IList<Entidade.Tarefa>> ListarTarefas();
    Task<Entidade.Tarefa> ListarTarefaId(int ?id);
}
