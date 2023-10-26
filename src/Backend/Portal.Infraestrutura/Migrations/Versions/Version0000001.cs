using FluentMigrator;

namespace Portal.Infraestrutura.Migrations.Versions;
[Migration((long)VersionList.CriarTabelaUsuario, "Função em criar a tabela de Usuario do sistema")]
public class Version0000001 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        var tabela = Base.ColunaPadrao(Create.Table("Usuarios"));

        tabela
            .WithColumn("Nome").AsString(100).NotNullable()
            .WithColumn("Email").AsString(100).NotNullable()
            .WithColumn("Senha").AsString(2000).NotNullable()
            .WithColumn("Telefone").AsString(14).NotNullable();
    }
}
