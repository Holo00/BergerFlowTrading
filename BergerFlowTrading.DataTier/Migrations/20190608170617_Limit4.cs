using Microsoft.EntityFrameworkCore.Migrations;

namespace BergerFlowTrading.DataTier.Migrations
{
    public partial class Limit4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BaseCurrency_Share_Percentage",
                table: "LimitArbitrageStrategy4Settings",
                type: "decimal(18, 10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Max_Price",
                table: "LimitArbitrageStrategy4Settings",
                type: "decimal(18, 10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Min_Price",
                table: "LimitArbitrageStrategy4Settings",
                type: "decimal(18, 10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ApiTimeoutMilliseconds",
                table: "Exchanges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DelayBetweenCallMilliseonds",
                table: "Exchanges",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RateLimitIntervalSeconds",
                table: "Exchanges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RateMaxQuantity",
                table: "Exchanges",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseCurrency_Share_Percentage",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropColumn(
                name: "Max_Price",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropColumn(
                name: "Min_Price",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropColumn(
                name: "ApiTimeoutMilliseconds",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "DelayBetweenCallMilliseonds",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "RateLimitIntervalSeconds",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "RateMaxQuantity",
                table: "Exchanges");
        }
    }
}
