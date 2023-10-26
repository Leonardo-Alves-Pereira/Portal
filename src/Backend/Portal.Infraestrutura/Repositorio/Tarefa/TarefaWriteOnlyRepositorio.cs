using Microsoft.EntityFrameworkCore;
using Portal.Domain.Repositorio.Tarefa;
using Portal.Infraestrutura.AcessoRepositorio;

namespace Portal.Infraestrutura.Repositorio.Tarefa;

public class TarefaWriteOnlyRepositorioUpdate : ITarefaWriteOnlyRepositorio
{
    private readonly PortalContexto _contexto;

    public TarefaWriteOnlyRepositorioUpdate(PortalContexto contexto)
    {
        _contexto = contexto;
    }
    public async Task Registrar(Domain.Entidade.Tarefa Tarefa)
    {
        await _contexto.Tarefa.AddAsync(Tarefa);
    }
}
