using FluentMigrator;

namespace Route256.MerchandiseService.Migrator.Migrations
{
    public class EmployeeTable : Migration
    {
        
        public override void Up()
        {
            Create.Table("employee")
                .WithColumn("Id").AsString().Identity().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("Position").AsString().NotNullable()
                .WithColumn("WorkExperience").AsString().NotNullable()
                ;
        }

        public override void Down()
        {
            Delete.Table("employee");
        }
    }
}