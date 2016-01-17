using Microsoft.Data.Entity.Migrations;

namespace BuyScanModels.Migrations
{
    public partial class AddIsProcessedToReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "Receipt",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "IsProcessed", table: "Receipt");
        }
    }
}
