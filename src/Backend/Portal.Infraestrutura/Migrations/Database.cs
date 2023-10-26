using Dapper;
using Microsoft.Data.SqlClient;

namespace Portal.Infraestrutura.Migrations
{
    public static class Database
    {
        public static void CriarDatabase(string conexaoComBancoDeDados, string nomeDatabase)
        {
            using var minhaConexao = new SqlConnection(conexaoComBancoDeDados);
            var registros = minhaConexao.ExecuteScalar($"IF DB_ID('{nomeDatabase}') IS NOT NULL SELECT 1 ELSE SELECT 0");

            if (registros.Equals(0))
            {
                minhaConexao.Execute($"CREATE DATABASE {nomeDatabase}");
            }
        }
    }
}
