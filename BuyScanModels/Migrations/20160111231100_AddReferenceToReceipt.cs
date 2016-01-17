using Microsoft.Data.Entity.Migrations;

namespace BuyScanModels.Migrations
{
    public partial class AddReferenceToReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Receipt",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Reference", table: "Receipt");
        }
    }
}
