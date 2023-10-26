using AutoMapper;
using Portal.Comunicacao.Resposta;
using Portal.Domain.Repositorio;
using Portal.Domain.Repositorio.Tarefa;

namespace Portal.Application.UseCases.Tarefa.Listar;


public class ListarTarefaUseCase : IListarTarefaUseCase
{
    private IMapper _mapper;
    private IUnidadeDeTrabalho _unidadeDeTrabalho;
    private ITarefaReadOnlyRepositorio _repositorio;

    public ListarTarefaUseCase(IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, ITarefaReadOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _repositorio = repositorio;
    }

    public async Task<IList<RespostaTarefaJson>> Executar()
    {
        var tarefas = await _repositorio.ListarTarefas();
        return _mapper.Map<List<RespostaTarefaJson>>(tarefas);
    }
}