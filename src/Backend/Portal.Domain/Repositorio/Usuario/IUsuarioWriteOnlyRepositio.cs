using Portal.Domain.Entidade;

namespace Portal.Domain.Repositorio.Usuario;

public interface IUsuarioWriteOnlyRepositio
{
    Task Adicionar(Entidade.Usuario usuario);
}
