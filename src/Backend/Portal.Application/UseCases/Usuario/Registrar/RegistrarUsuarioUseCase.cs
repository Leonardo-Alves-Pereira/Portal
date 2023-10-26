using AutoMapper;
using Portal.Application.Servicos.Criptografia;
using Portal.Application.Servicos.Token;
using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;
using Portal.Domain.Repositorio;
using Portal.Domain.Repositorio.Usuario;
using Portal.Exceptions.ExceptionBase;
using Portal.Exceptions.Resources;

namespace Portal.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioUseCase : IRegistrarUsuarioUseCase
{
    private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
    private readonly IUsuarioWriteOnlyRepositio _repositorio;
    private readonly IMapper _mapper;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;


    public RegistrarUsuarioUseCase(IUsuarioWriteOnlyRepositio repositorio, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho,
        EncriptadorDeSenha encriptadorDeSenha, TokenController tokenController, IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio)
    {
        _repositorio = repositorio;
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
        _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
    }
    public async Task<RespostaUsuarioRegistradoJson> Executar(RequisicaoRegistrarUsuarioJson requisicao)
    {
        // Validar Request.
        await Validar(requisicao);

        // Salvar dados no banco.
        var entidade = _mapper.Map<Domain.Entidade.Usuario>(requisicao);
        entidade.Senha = _encriptadorDeSenha.Criptografar(requisicao.Senha);

        await _repositorio.Adicionar(entidade);
        await _unidadeDeTrabalho.Commit();

        var token = _tokenController.GerarToken(entidade.Email);

        return new RespostaUsuarioRegistradoJson
        {
            Token = token
        };
    }
    public async Task Validar(RequisicaoRegistrarUsuarioJson requisicao)
    {
        var validacao = new RegistrarUsuarioValidator();
        var resultado = validacao.Validate(requisicao);
        var existeUsuarioEmail = await _usuarioReadOnlyRepositorio.ExisteEmail(requisicao.Email);

        if (existeUsuarioEmail)
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceErrorMessage.EMAIL_EXISTE));


        var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
        if (!resultado.IsValid)
            throw new ErroDeValidacaoException(mensagensDeErro);
    }
}
