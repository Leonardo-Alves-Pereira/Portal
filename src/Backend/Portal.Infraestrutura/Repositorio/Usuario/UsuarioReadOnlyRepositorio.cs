using Microsoft.EntityFrameworkCore;
using Portal.Domain.Repositorio.Usuario;
using Portal.Infraestrutura.AcessoRepositorio;

namespace Portal.Infraestrutura.Repositorio.Usuario;

public class UsuarioReadOnlyRepositorio : IUsuarioReadOnlyRepositorio
{
    private readonly PortalContexto _contexto;

    public UsuarioReadOnlyRepositorio(PortalContexto contexto)
    {
        _contexto = contexto;
    }

    public Task<bool> ExisteEmail(string email)
    {
        return _contexto.Usuarios.AnyAsync(x => x.Email.Equals(email));
    }

    public async Task<Domain.Entidade.Usuario> RecuperarPorEmail(string email)
    {
        return await _contexto.Usuarios.AsNoTracking()
                                       .FirstOrDefaultAsync(c => c.Email.Equals(email));
    }

    public async Task<Domain.Entidade.Usuario> RecuperarPorEmailSenha(string email, string senha)
    {
        return await _contexto.Usuarios.AsNoTracking()
                                       .FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Senha.Equals(senha));
    }
}
