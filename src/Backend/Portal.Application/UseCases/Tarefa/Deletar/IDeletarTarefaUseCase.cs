using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;

namespace Portal.Application.UseCases.Tarefa.Deletar;

public interface IDeletarTarefaUseCase
{
    Task<RespostaTarefaJson> Executar(GenericRequestIdJson id);
}
