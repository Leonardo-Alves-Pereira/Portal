using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portal.Domain.Extension;
using Portal.Domain.Repositorio;
using Portal.Domain.Repositorio.Tarefa;
using Portal.Domain.Repositorio.Usuario;
using Portal.Infraestrutura.AcessoRepositorio;
using Portal.Infraestrutura.Repositorio.Tarefa;
using Portal.Infraestrutura.Repositorio.Usuario;
using System.Reflection;

namespace Portal.Infraestrutura;

public static class Bootstraped
{
    public static void AddInfraestrutura(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddFluentMigrator(services, configurationManager);
        AddRepositorios(services);
        AddUnidadeDeTrabalho(services);
        AddContexto(services, configurationManager);
    }

    private static void AddUnidadeDeTrabalho(IServiceCollection services)
    {
        services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
    }

    private static void AddContexto(IServiceCollection services, IConfiguration configurationManager)
    {
        var connectionString = configurationManager.GetConexaoCompleta();

        services.AddDbContext<PortalContexto>(contextoOpcoes =>
        {
            contextoOpcoes.UseSqlServer(connectionString);
        });
    }

    private static void AddRepositorios(IServiceCollection services)
    {
        services.AddScoped<IUsuarioWriteOnlyRepositio, UsuarioWriteOnlyRepositio>()
                .AddScoped<IUsuarioReadOnlyRepositorio, UsuarioReadOnlyRepositorio>()
                .AddScoped<IUsuarioUpdateOnlyRepositorio, UsuarioUpdateOnlyRepositorio>()

                .AddScoped<ITarefaWriteOnlyRepositorio, TarefaWriteOnlyRepositorioUpdate>()
                .AddScoped<ITarefaReadOnlyRepositorio, TarefaReadOnlyRepositorio>()
                .AddScoped<ITarefaUpdateOnlyRepositorio, TarefaUpdateOnlyRepositorio>()
                .AddScoped<ITarefaDeleteOnlyRepositorio, TarefaDeleteOnlyRepositorio>();
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager)
    {
        services
            .AddFluentMigratorCore()
            .ConfigureRunner(c => c.AddSqlServer()
            .WithGlobalConnectionString(configurationManager.GetConexaoCompleta())
            .ScanIn(Assembly
            .Load("Portal.Infraestrutura")).For.All());
    }
}
