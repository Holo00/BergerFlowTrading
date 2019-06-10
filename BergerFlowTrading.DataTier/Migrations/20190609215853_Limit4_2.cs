using Microsoft.EntityFrameworkCore.Migrations;

namespace BergerFlowTrading.DataTier.Migrations
{
    public partial class Limit4_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "USD_Value_To_Trade",
                table: "LimitArbitrageStrategy4Settings",
                newName: "Value_To_Trade_Min");

            migrationBuilder.AddColumn<int>(
                name: "Value_Currency",
                table: "LimitArbitrageStrategy4Settings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Value_To_Trade_Max",
                table: "LimitArbitrageStrategy4Settings",
                type: "decimal(18, 10)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value_Currency",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropColumn(
                name: "Value_To_Trade_Max",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.RenameColumn(
                name: "Value_To_Trade_Min",
                table: "LimitArbitrageStrategy4Settings",
                newName: "USD_Value_To_Trade");
        }
    }
}
