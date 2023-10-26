using Microsoft.EntityFrameworkCore;
using Portal.Domain.Repositorio.Tarefa;
using Portal.Infraestrutura.AcessoRepositorio;

namespace Portal.Infraestrutura.Repositorio.Tarefa;

public class TarefaDeleteOnlyRepositorio : ITarefaDeleteOnlyRepositorio
{
    private readonly PortalContexto _contexto;

    public TarefaDeleteOnlyRepositorio(PortalContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<Domain.Entidade.Tarefa> RecuperarPorId(int? id)
    {
        return await _contexto.Tarefa.AsNoTracking()
                                        .Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public void Deletar(Domain.Entidade.Tarefa Tarefa)
    {
        _contexto.Tarefa.Remove(Tarefa);
    }
}
