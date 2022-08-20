namespace Desk.Migrations.Migrations;

[CustomMigration(1, 1, "Create 'Project' table")]
[Tags("Desk")]
public class CreateProjectTable : Migration
{
    public override void Up()
    {
        Create.Table("Project")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(256).NotNullable()
            .WithColumn("Description").AsString(1024).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Project");
    }
}