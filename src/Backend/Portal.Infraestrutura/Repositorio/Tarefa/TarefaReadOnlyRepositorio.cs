using Microsoft.EntityFrameworkCore;
using Portal.Domain.Repositorio.Tarefa;
using Portal.Infraestrutura.AcessoRepositorio;

namespace Portal.Infraestrutura.Repositorio.Tarefa;

public class TarefaReadOnlyRepositorio : ITarefaReadOnlyRepositorio
{
    private readonly PortalContexto _contexto;

    public TarefaReadOnlyRepositorio(PortalContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<Domain.Entidade.Tarefa> ListarTarefaId(int? id)
    {
        return await _contexto.Tarefa.AsNoTracking().Include(u => u.Usuario)
                                        .Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IList<Domain.Entidade.Tarefa>> ListarTarefas()
    {
        return await _contexto.Tarefa.AsNoTracking().Include(u => u.Usuario).ToListAsync();
    }
}
