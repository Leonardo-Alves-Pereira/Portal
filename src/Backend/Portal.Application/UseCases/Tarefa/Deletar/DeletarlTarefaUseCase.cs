using AutoMapper;
using Portal.Application.Servicos.UsuarioLogado;
using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;
using Portal.Domain.Repositorio;
using Portal.Domain.Repositorio.Tarefa;
using Portal.Exceptions.ExceptionBase;

namespace Portal.Application.UseCases.Tarefa.Deletar;

public class DeletarlTarefaUseCase : IDeletarTarefaUseCase
{
    private IMapper _mapper;
    private IUnidadeDeTrabalho _unidadeDeTrabalho;
    private ITarefaDeleteOnlyRepositorio _repositorio;


    public DeletarlTarefaUseCase(IMapper mapper, 
                               IUnidadeDeTrabalho unidadeDeTrabalho, 
                               ITarefaDeleteOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _repositorio = repositorio;
    }
    public async Task<RespostaTarefaJson> Executar(GenericRequestIdJson id)
    {
        var validacao = new TarefaValidator();
        var resultado = validacao.Validate(id.Id);
        var tarefaRecuperado = await _repositorio.RecuperarPorId(id.Id);
        var Tarefa = _mapper.Map<Domain.Entidade.Tarefa>(tarefaRecuperado);

        var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
        if (!resultado.IsValid)
            throw new ErroTarefaException(mensagensDeErro);

        _repositorio.Deletar(Tarefa);
        await _unidadeDeTrabalho.Commit();
        return _mapper.Map<RespostaTarefaJson>(Tarefa);
    }

    private void Validar(RequisicaoTarefaJson requisicao)
    {
        var validator = new DeletarTarefaValidator();
        var resultado = validator.Validate(requisicao);

        var mensagesDeErro = resultado.Errors.Select(e => e.ErrorMessage).ToList();
        if (!resultado.IsValid)
            throw new ErroDeValidacaoException(mensagesDeErro);
    }
}
