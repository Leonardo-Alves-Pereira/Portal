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
    private ITarefaUpdateOnlyRepositorio _repositorio;


    public AtualizarTarefaUseCase(IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, ITarefaUpdateOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _repositorio = repositorio;
    }
    public async Task<RequisicaoTarefaJson> Executar(RequisicaoTarefaJson requisicao)
    {
        Validar(requisicao);

        var Tarefa = _mapper.Map<Domain.Entidade.Tarefa>(requisicao);

        _repositorio.Update(Tarefa);
        await _unidadeDeTrabalho.Commit();
        return requisicao;
    }

    private void Validar(RequisicaoTarefaJson requisicao)
    {
        var validator = new RegistrarTarefaValidator();
        var resultado = validator.Validate(requisicao);

        var mensagesDeErro = resultado.Errors.Select(e =>
        {
            var erroNome = e.PropertyName;
            var mensagem = e.ErrorMessage;
            return new ErroValidacaoJson
            {
                ErroNome = erroNome,
                Mensagem = mensagem
            };
              
        }).ToList();

        if (!resultado.IsValid)
            throw new ErroGenericoException(mensagesDeErro);
    }
}
