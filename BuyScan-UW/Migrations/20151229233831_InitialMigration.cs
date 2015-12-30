using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace BuyScan_UW.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receipt",
                columns: table => new
                {
                    ReceiptId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => x.ReceiptId);
                });
            migrationBuilder.CreateTable(
                name: "ReceiptItem",
                columns: table => new
                {
                    ReceiptItemId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ReceiptReceiptId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptItem", x => x.ReceiptItemId);
                    table.ForeignKey(
                        name: "FK_ReceiptItem_Receipt_ReceiptReceiptId",
                        column: x => x.ReceiptReceiptId,
                        principalTable: "Receipt",
                        principalColumn: "ReceiptId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("ReceiptItem");
            migrationBuilder.DropTable("Receipt");
        }
    }
}
