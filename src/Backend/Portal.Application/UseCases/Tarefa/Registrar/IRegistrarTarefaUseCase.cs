using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;

namespace Portal.Application.UseCases.Tarefa.Registrar;

public interface IRegistrarTarefaUseCase
{
    Task<RespostaTarefaJson> Executar(RequisicaoTarefaJson requisicao);
}
