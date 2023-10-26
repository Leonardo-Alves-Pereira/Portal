namespace Portal.Application.Servicos.UsuarioLogado;

public interface IUsuarioLogado
{
    Task<Domain.Entidade.Usuario> RecuperarUsuario();
}
