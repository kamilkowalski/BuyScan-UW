using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace BuyScan_UW.Migrations
{
    public partial class AddImagePathToReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Receipt",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "ImagePath", table: "Receipt");
        }
    }
}
