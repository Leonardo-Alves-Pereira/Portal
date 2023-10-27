using AutoMapper;
using Portal.Comunicacao.Resposta;
using Portal.Domain.Entidade;
using Portal.Domain.Repositorio;
using Portal.Domain.Repositorio.Tarefa;
using Portal.Exceptions.ExceptionBase;

namespace Portal.Application.UseCases.Tarefa.Listar;


public class ListarTarefaIdUseCase : IListarTarefaIdUseCase
{
    private IMapper _mapper;
    private IUnidadeDeTrabalho _unidadeDeTrabalho;
    private ITarefaReadOnlyRepositorio _repositorio;


    public ListarTarefaIdUseCase(IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, ITarefaReadOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _repositorio = repositorio;
    }

    public async Task<RespostaTarefaJson> Executar(int ?id)
    {
        var validacao = new TarefaValidator();
        var resultado = validacao.Validate(id.GetValueOrDefault());

        var existeTarefa = await _repositorio.ListarTarefaId(id);

        var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
        if (!resultado.IsValid)
            throw new ErroTarefaException(mensagensDeErro);

        return _mapper.Map<RespostaTarefaJson>(existeTarefa);
    }
}


