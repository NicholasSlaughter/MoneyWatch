using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyWatch.API.Migrations
{
    public partial class fixed_PeriodEndDate_misspelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriodEndDate",
                table: "Alert",
                newName: "PeriodEndDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PeriodEndDate",
                table: "Alert",
                newName: "PriodEndDate");
        }
    }
}
