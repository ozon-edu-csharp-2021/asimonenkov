using FluentMigrator;

namespace Route256.MerchandiseService.Migrator.Migrations
{
    public class GiveOutMerchPackRequestTable : Migration
    {
        public override void Up()
        {
            Create.Table("give_out_merch_pack_request")
                .WithColumn("Id").AsString().Identity().PrimaryKey()
                .WithColumn("MercPackName").AsInt32().NotNullable()
                .WithColumn("GiveOutDate").AsDateTime().NotNullable()
                .WithColumn("Status").AsString().NotNullable()
                .WithColumn("EmployeeId").AsString().NotNullable();

        }

        public override void Down()
        {
            Delete.Table("give_out_merch_pack_request");
        }
    }
}