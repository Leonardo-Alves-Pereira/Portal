using Microsoft.Extensions.Configuration;

namespace Portal.Domain.Extension;

public static class RepositorioExtension
{
    public static string GetNomeDatabase(this IConfiguration configurationManager)
    {
        var nomeDatabase = configurationManager.GetConnectionString("NomeDatabase");
        return nomeDatabase;
    }

    public static string GetConexao(this IConfiguration configurationManager)
    {
        var conexao = configurationManager.GetConnectionString("Conexao");
        return conexao;
    }
    public static string GetConexaoCompleta(this IConfiguration configurationManager)
    {
        var conexao = configurationManager.GetConnectionString("Conexao");
        var nomeDatabase = configurationManager.GetConnectionString("NomeDatabase");
        return $"{conexao}Database={nomeDatabase}";
    }

}
