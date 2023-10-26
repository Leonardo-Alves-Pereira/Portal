using Portal.Domain.Repositorio.Usuario;
using Portal.Infraestrutura.AcessoRepositorio;

namespace Portal.Infraestrutura.Repositorio.Usuario;

public class UsuarioWriteOnlyRepositio : IUsuarioWriteOnlyRepositio
{
    private readonly PortalContexto _contexto;

    public UsuarioWriteOnlyRepositio(PortalContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task Adicionar(Domain.Entidade.Usuario usuario)
    {
        await _contexto.Usuarios.AddAsync(usuario);
    }
}
