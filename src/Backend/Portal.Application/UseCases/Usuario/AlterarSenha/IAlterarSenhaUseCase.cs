using Portal.Comunicacao.Requisicao;

namespace Portal.Application.UseCases.Usuario.AlterarSenha;

public interface IAlterarSenhaUseCase
{
    Task Executar(AlterarSenhaJson requisicao);
}
