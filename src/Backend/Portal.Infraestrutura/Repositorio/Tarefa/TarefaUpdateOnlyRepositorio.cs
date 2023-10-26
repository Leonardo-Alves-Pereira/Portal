using Microsoft.EntityFrameworkCore;
using Portal.Domain.Repositorio.Tarefa;
using Portal.Infraestrutura.AcessoRepositorio;

namespace Portal.Infraestrutura.Repositorio.Tarefa;

public class TarefaUpdateOnlyRepositorio : ITarefaUpdateOnlyRepositorio
{
    private readonly PortalContexto _contexto;

    public TarefaUpdateOnlyRepositorio(PortalContexto contexto)
    {
        _contexto = contexto;
    }

    public void Update(Domain.Entidade.Tarefa Tarefa)
    {
        _contexto.Tarefa.Update(Tarefa);
    }
}
