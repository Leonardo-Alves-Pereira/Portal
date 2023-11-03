using FluentValidation;
using Portal.Application.Servicos.Criptografia;
using Portal.Application.Servicos.UsuarioLogado;
using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;
using Portal.Domain.Repositorio;
using Portal.Domain.Repositorio.Usuario;
using Portal.Exceptions.ExceptionBase;
using Portal.Exceptions.Resources;
using System.ComponentModel.DataAnnotations;

namespace Portal.Application.UseCases.Usuario.AlterarSenha;

public class AlterarSenhaUseCase : IAlterarSenhaUseCase
{
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IUsuarioUpdateOnlyRepositorio _repositorio;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    public AlterarSenhaUseCase(IUsuarioUpdateOnlyRepositorio repositorio, 
                               IUsuarioLogado usuarioLogado, 
                               EncriptadorDeSenha encriptadorDeSenha,
                               IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado;
        _encriptadorDeSenha = encriptadorDeSenha;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }
    public async Task Executar(AlterarSenhaJson requisicao)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var usuario = await _repositorio.RecuperarPorId(usuarioLogado.Id);

        Validar(requisicao, usuario);

        usuario.Senha = _encriptadorDeSenha.Criptografar(requisicao.NovaSenha);

        _repositorio.Update(usuario);

        await _unidadeDeTrabalho.Commit();
    }

    public void Validar(AlterarSenhaJson requisicao, Domain.Entidade.Usuario usuario)
    {
        var validator = new AlterarSenhaValidator();
        var resultado = validator.Validate(requisicao);
        var senhaCriptografada = _encriptadorDeSenha.Criptografar(requisicao.SenhaAtual);

        if (!usuario.Senha.Equals(senhaCriptografada))
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("senhaAtual", ResourceErrorMessage.SENHA_ATUAL_INVALIDA));

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
