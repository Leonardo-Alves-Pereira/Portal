using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portal.Domain.Repositorio.Tarefa;

public interface ITarefaDeleteOnlyRepositorio
{
    public Task<Domain.Entidade.Tarefa> RecuperarPorId(int ?id);
    public void Deletar(Entidade.Tarefa Tarefa);
}
