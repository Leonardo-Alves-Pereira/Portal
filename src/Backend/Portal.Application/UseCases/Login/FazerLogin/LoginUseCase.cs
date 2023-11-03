using AutoMapper;
using Portal.Application.Servicos.Criptografia;
using Portal.Application.Servicos.Token;
using Portal.Application.UseCases.Usuario.Registrar;
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
    private readonly IMapper _mapper;

    public LoginUseCase(IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, EncriptadorDeSenha encriptadorDeSenha, TokenController tokenController, IMapper mapper)
    {
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
        _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
        _mapper = mapper;
    }
    public async Task<RespostaLoginJson> Executar(RequisicaoLoginJson request)
    {
        var criptografarSenha = _encriptadorDeSenha.Criptografar(request.Senha);
        var usuario = await _usuarioReadOnlyRepositorio.RecuperarPorEmailSenha(request.Email, criptografarSenha);
        var mapUser = _mapper.Map<RequisicaoUsuarioJson>(usuario);

        if (usuario == null)
            Validar(mapUser);

        return new RespostaLoginJson
        {
            id = usuario.Id,
            Nome = usuario.Nome,
            Token = _tokenController.GerarToken(usuario.Email),
        };
    }

    private void Validar(RequisicaoUsuarioJson usuario)
    {
        usuario = new RequisicaoUsuarioJson();
        var validator = new LoginValidator();
        var resultado = validator.Validate(usuario);
        var mensagensDeErro = resultado.Errors.Select(error =>
        {
            return new ErroValidacaoJson
            {
                ErroNome = error.PropertyName,
                Mensagem = error.ErrorMessage
            };
        }).ToList();

        if (!resultado.IsValid)
            throw new ErroGenericoException(mensagensDeErro);
    }
}
