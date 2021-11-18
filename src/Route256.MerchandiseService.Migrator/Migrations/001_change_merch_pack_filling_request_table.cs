using FluentMigrator;

namespace Route256.MerchandiseService.Migrator.Migrations
{
    public class ChangeMerchPackFillingRequestTable : Migration
    {
        public override void Up()
        {
            Create.Table("change_merch_pack_filling_request")
                .WithColumn("Id").AsString().Identity().PrimaryKey()
                .WithColumn("MercPackName").AsInt32().NotNullable()
                .WithColumn("ChangeDate").AsDateTime().NotNullable()
                .WithColumn("AdditionalItems").AsInt32().NotNullable();

        }

        public override void Down()
        {
            Delete.Table("change_merch_pack_filling_request");
        }
    }
}