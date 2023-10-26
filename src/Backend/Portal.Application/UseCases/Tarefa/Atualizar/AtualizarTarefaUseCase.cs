using AutoMapper;
using Portal.Application.UseCases.Tarefa.Registrar;
using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;
using Portal.Domain.Repositorio;
using Portal.Domain.Repositorio.Tarefa;
using Portal.Exceptions.ExceptionBase;

namespace Portal.Application.UseCases.Tarefa.Atualizar;

public class AtualizarTarefaUseCase : IAtualizarTarefaUseCase
{
    private IMapper _mapper;
    private IUnidadeDeTrabalho _unidadeDeTrabalho;
    private ITarefaWriteOnlyRepositorio _repositorio;


    public AtualizarTarefaUseCase(IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, ITarefaWriteOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _repositorio = repositorio;
    }
    public async Task<RespostaTarefaJson> Executar(RequisicaoTarefaJson requisicao)
    {
        Validar(requisicao);

        var Tarefa = _mapper.Map<Domain.Entidade.Tarefa>(requisicao);

        await _repositorio.Registrar(Tarefa);
        await _unidadeDeTrabalho.Commit();
        return _mapper.Map<RespostaTarefaJson>(Tarefa);
    }

    private void Validar(RequisicaoTarefaJson requisicao)
    {
        var validator = new RegistrarTarefaValidator();
        var resultado = validator.Validate(requisicao);

        var mensagesDeErro = resultado.Errors.Select(e => e.ErrorMessage).ToList();
        if (!resultado.IsValid)
            throw new ErroDeValidacaoException(mensagesDeErro);
    }
}
