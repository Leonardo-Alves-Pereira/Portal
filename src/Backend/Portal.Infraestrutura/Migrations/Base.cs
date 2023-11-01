using FluentMigrator.Builders.Create.Table;

namespace Portal.Infraestrutura.Migrations;

public static class Base
{
    public static ICreateTableColumnOptionOrWithColumnSyntax ColunaPadrao(ICreateTableWithColumnOrSchemaOrDescriptionSyntax tabela)
    {
        return tabela
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("DataCriacao").AsString(100).NotNullable();

    }
}
