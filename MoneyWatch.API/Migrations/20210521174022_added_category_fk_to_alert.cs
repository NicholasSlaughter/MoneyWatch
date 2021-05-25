using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyWatch.API.Migrations
{
    public partial class added_category_fk_to_alert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Alert",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId1",
                table: "Alert",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alert_CategoryId1",
                table: "Alert",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Alert_Category_CategoryId1",
                table: "Alert",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alert_Category_CategoryId1",
                table: "Alert");

            migrationBuilder.DropIndex(
                name: "IX_Alert_CategoryId1",
                table: "Alert");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Alert");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Alert");
        }
    }
}
