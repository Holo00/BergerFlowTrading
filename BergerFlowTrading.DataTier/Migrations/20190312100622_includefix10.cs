using Microsoft.EntityFrameworkCore.Migrations;

namespace BergerFlowTrading.DataTier.Migrations
{
    public partial class includefix10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_ID_1",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.AlterColumn<int>(
                name: "Exchange_ID_1",
                table: "LimitArbitrageStrategy4Settings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Exchange_ID_2",
                table: "LimitArbitrageStrategy4Settings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LimitArbitrageStrategy4Settings_Exchange_ID_2",
                table: "LimitArbitrageStrategy4Settings",
                column: "Exchange_ID_2");

            migrationBuilder.AddForeignKey(
                name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_ID_1",
                table: "LimitArbitrageStrategy4Settings",
                column: "Exchange_ID_1",
                principalTable: "Exchanges",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_ID_2",
                table: "LimitArbitrageStrategy4Settings",
                column: "Exchange_ID_2",
                principalTable: "Exchanges",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_ID_1",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropForeignKey(
                name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_ID_2",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropIndex(
                name: "IX_LimitArbitrageStrategy4Settings_Exchange_ID_2",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropColumn(
                name: "Exchange_ID_2",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.AlterColumn<int>(
                name: "Exchange_ID_1",
                table: "LimitArbitrageStrategy4Settings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_ID_1",
                table: "LimitArbitrageStrategy4Settings",
                column: "Exchange_ID_1",
                principalTable: "Exchanges",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
