using System.Threading.Tasks;

namespace Portal.Domain.Repositorio.Usuario;

public interface IUsuarioReadOnlyRepositorio
{
    Task<bool> ExisteEmail(string email);
    Task<Entidade.Usuario> RecuperarPorEmail(string email);
    Task<Entidade.Usuario> RecuperarPorEmailSenha(string email, string senha);
}
