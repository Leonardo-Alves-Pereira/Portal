using AutoMapper;
using Portal.Application.Servicos.UsuarioLogado;
using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;
using Portal.Domain.Repositorio;
using Portal.Domain.Repositorio.Tarefa;
using Portal.Exceptions.ExceptionBase;

namespace Portal.Application.UseCases.Tarefa.Registrar;

public class RegistrarTarefaUseCase : IRegistrarTarefaUseCase
{
    private IMapper _mapper;
    private IUnidadeDeTrabalho _unidadeDeTrabalho;
    private ITarefaWriteOnlyRepositorio _repositorio;


    public RegistrarTarefaUseCase(IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, ITarefaWriteOnlyRepositorio repositorio)
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
