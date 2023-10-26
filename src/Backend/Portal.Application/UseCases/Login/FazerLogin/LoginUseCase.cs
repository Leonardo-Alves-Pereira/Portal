using Portal.Application.Servicos.Criptografia;
using Portal.Application.Servicos.Token;
using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;
using Portal.Domain.Repositorio.Usuario;
using Portal.Exceptions.ExceptionBase;

namespace Portal.Application.UseCases.Login.FazerLogin;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;

    public LoginUseCase(IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, EncriptadorDeSenha encriptadorDeSenha, TokenController tokenController)
    {
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
        _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
    }
    public async Task<RespostaLoginJson> Executar(RequisicaoLoginJson request)
    {
        var criptografarSenha = _encriptadorDeSenha.Criptografar(request.Senha);
        var usuario = await _usuarioReadOnlyRepositorio.RecuperarPorEmailSenha(request.Email, criptografarSenha);

        if (usuario == null)
            throw new LoginInvalidoException();

        return new RespostaLoginJson
        {
            Nome = usuario.Nome,
            Token = _tokenController.GerarToken(usuario.Email),
        };
    }
}
