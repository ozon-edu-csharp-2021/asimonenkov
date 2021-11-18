using FluentMigrator;

namespace Route256.MerchandiseService.Migrator.Migrations
{
    public class MerchPackTable : Migration
    {
        
        public override void Up()
        {
            Create.Table("merch_pack")
                .WithColumn("Id").AsString().Identity().PrimaryKey()
                .WithColumn("MerchItemIds").AsString().NotNullable()
                .WithColumn("Name").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("merch_pack");
        }
    }
}