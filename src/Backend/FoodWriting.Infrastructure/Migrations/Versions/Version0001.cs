using FluentMigrator;

namespace FoodWriting.Infrastructure.Migrations.Versions;

[Migration(1, "Create User Table")]
public class Version0001 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        var table = Create.Table("User");

        table
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("Password").AsString(2000).NotNullable()
            .WithColumn("Phone").AsString(14).NotNullable();
    }
}
