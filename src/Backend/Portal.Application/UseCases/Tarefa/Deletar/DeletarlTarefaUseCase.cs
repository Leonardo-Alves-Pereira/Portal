using AutoMapper;
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
    public async Task<RespostaTarefaJson> Executar(RequisicaoTarefaJson id)
    {
        var tarefaRecuperado = await _repositorio.RecuperarPorId(id.Id);
        var Tarefa = _mapper.Map<RequisicaoTarefaJson>(tarefaRecuperado);

        Validar(Tarefa);

        _repositorio.Deletar(tarefaRecuperado);
        await _unidadeDeTrabalho.Commit();
        return _mapper.Map<RespostaTarefaJson>(tarefaRecuperado);
    }

    private void Validar(RequisicaoTarefaJson requisicao)
    {
        if (requisicao == null)
            requisicao = new RequisicaoTarefaJson();
    
        var validator = new DeletarTarefaValidator();
        var resultado = validator.Validate(requisicao);
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
