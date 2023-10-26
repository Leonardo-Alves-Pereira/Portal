using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;

namespace Portal.Application.UseCases.Usuario.Registrar;

public interface IRegistrarUsuarioUseCase
{
    Task<RespostaUsuarioRegistradoJson> Executar(RequisicaoRegistrarUsuarioJson requisicao);

    Task Validar(RequisicaoRegistrarUsuarioJson requisicao);
}
