using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;

namespace Portal.Application.UseCases.Tarefa.Atualizar;

public interface IAtualizarTarefaUseCase
{
    Task<RequisicaoTarefaJson> Executar(RequisicaoTarefaJson requisicao);
}
