using Microsoft.EntityFrameworkCore;
using Portal.Domain.Repositorio.Usuario;
using Portal.Infraestrutura.AcessoRepositorio;

namespace Portal.Infraestrutura.Repositorio.Usuario;

public class UsuarioUpdateOnlyRepositorio : IUsuarioUpdateOnlyRepositorio
{
    private readonly PortalContexto _contexto;
    public UsuarioUpdateOnlyRepositorio(PortalContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<Domain.Entidade.Usuario> RecuperarPorId(long id)
    {
        return await _contexto.Usuarios.FirstOrDefaultAsync(c => c.Id == id);
    }

    public void Update(Domain.Entidade.Usuario usuario)
    {
        _contexto.Usuarios.Update(usuario);
    }
}
