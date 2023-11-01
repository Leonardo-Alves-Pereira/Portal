using FluentMigrator;

namespace Portal.Infraestrutura.Migrations.Versions;
[Migration((long)VersionList.CriarTabelaTarefas, "Função em criar a tabela de tarefas e categoria do sistema")]
public class Version0000002 : Migration
{
    public override void Up()
    {
        CriarTabelaTarefas();
    }

    private void CriarTabelaTarefas()
    {
        var tabela = Base.ColunaPadrao(Create.Table("Tarefas"));

        tabela
            .WithColumn("Titulo").AsString(100).NotNullable()
            .WithColumn("Descricao").AsString(2000).NotNullable()
            .WithColumn("DataConclusao").AsString(100).NotNullable()
            .WithColumn("UsuarioId").AsInt64().NotNullable().ForeignKey("FK_Tarefas_Usuario_Id", "Usuarios", "Id");
    }

    public override void Down() { }
}
