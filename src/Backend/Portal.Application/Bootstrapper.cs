using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portal.Application.Servicos.Criptografia;
using Portal.Application.Servicos.Token;
using Portal.Application.Servicos.UsuarioLogado;
using Portal.Application.UseCases.Login.FazerLogin;
using Portal.Application.UseCases.Tarefa.Atualizar;
using Portal.Application.UseCases.Tarefa.Deletar;
using Portal.Application.UseCases.Tarefa.Listar;
using Portal.Application.UseCases.Usuario.AlterarSenha;
using Portal.Application.UseCases.Usuario.Registrar;

namespace Portal.Application;

public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AdicionarChaveAdicionalSenha(services, configuration);
        AdicionarTokenJWT(services, configuration);
        AdicionarUseCases(services);
        AdicionarUsuarioLogado(services);
    }

    private static void AdicionarUsuarioLogado(IServiceCollection services)
    {
        services.AddScoped<IUsuarioLogado, UsuarioLogado>();
    }

    private static void AdicionarChaveAdicionalSenha(this IServiceCollection services, IConfiguration configuration)
    {
        var configuracao = configuration.GetRequiredSection("Configuracoes:Senha:ChaveAdicionalSenha");
        services.AddScoped(option => new EncriptadorDeSenha(configuracao.Value));

    }

    private static void AdicionarTokenJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var configuracaoTempoVidaToken = configuration.GetRequiredSection("Configuracoes:Jwt:TempoTokenMinutos");
        var configuracaoChaveToken = configuration.GetRequiredSection("Configuracoes:Jwt:ChaveToken");

        services.AddScoped(option => new TokenController(int.Parse(configuracaoTempoVidaToken.Value), configuracaoChaveToken.Value));

    }

    private static void AdicionarUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>()
                .AddScoped<ILoginUseCase, LoginUseCase>()
                .AddScoped<IAlterarSenhaUseCase, AlterarSenhaUseCase>()

                .AddScoped<IAtualizarTarefaUseCase, AtualizarTarefaUseCase>()
                .AddScoped<IListarTarefaUseCase, ListarTarefaUseCase>()
                .AddScoped<IListarTarefaIdUseCase, ListarTarefaIdUseCase>()
                .AddScoped<IAtualizarTarefaUseCase, AtualizarTarefaUseCase>()
                .AddScoped<IDeletarTarefaUseCase, DeletarlTarefaUseCase>();
    }
}
