namespace Portal.Domain.Repositorio.Usuario;

public interface IUsuarioUpdateOnlyRepositorio
{
    void Update(Entidade.Usuario usuario);
    Task<Entidade.Usuario> RecuperarPorId(long id);
}
