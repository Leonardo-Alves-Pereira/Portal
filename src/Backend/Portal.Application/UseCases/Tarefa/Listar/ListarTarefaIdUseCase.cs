using AutoMapper;
using Portal.Comunicacao.Resposta;
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

    public async Task<RespostaTarefaJson> Executar(int? id)
    {
        return await Validar(id);
    }

    public async Task<RespostaTarefaJson> Validar(int? id)
    {
        var validacao = new TarefaValidator();
        var resultado = validacao.Validate(id.GetValueOrDefault());
        var existeTarefa = await _repositorio.ListarTarefaId(id);

        if (existeTarefa == null)
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("tarefaInexistente", "Tarefa solicitada não existe"));

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

        return _mapper.Map<RespostaTarefaJson>(existeTarefa);
    }
}


