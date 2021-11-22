using FluentMigrator;

namespace Route256.MerchandiseService.Migrator.Migrations
{
    public class MerchItemTable : Migration
    {
        
        public override void Up()
        {
            Create.Table("merch_item")
                .WithColumn("Id").AsString().Identity().PrimaryKey()
                .WithColumn("ClothingSize").AsString().NotNullable()
                .WithColumn("Colour").AsString().NotNullable()
                .WithColumn("ItemType").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("merch_item");
        }
    }
}