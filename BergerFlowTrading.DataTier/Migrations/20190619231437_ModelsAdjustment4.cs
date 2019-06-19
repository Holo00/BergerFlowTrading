using Microsoft.EntityFrameworkCore.Migrations;

namespace BergerFlowTrading.DataTier.Migrations
{
    public partial class ModelsAdjustment4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_2ID",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropIndex(
                name: "IX_LimitArbitrageStrategy4Settings_Exchange_2ID",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropColumn(
                name: "Exchange_2ID",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.CreateIndex(
                name: "IX_LimitArbitrageStrategy4Settings_Exchange_ID_2",
                table: "LimitArbitrageStrategy4Settings",
                column: "Exchange_ID_2");

            migrationBuilder.AddForeignKey(
                name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_ID_2",
                table: "LimitArbitrageStrategy4Settings",
                column: "Exchange_ID_2",
                principalTable: "Exchanges",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_ID_2",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropIndex(
                name: "IX_LimitArbitrageStrategy4Settings_Exchange_ID_2",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.AddColumn<int>(
                name: "Exchange_2ID",
                table: "LimitArbitrageStrategy4Settings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LimitArbitrageStrategy4Settings_Exchange_2ID",
                table: "LimitArbitrageStrategy4Settings",
                column: "Exchange_2ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_2ID",
                table: "LimitArbitrageStrategy4Settings",
                column: "Exchange_2ID",
                principalTable: "Exchanges",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
