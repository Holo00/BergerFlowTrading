using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BergerFlowTrading.DataTier.Migrations
{
    public partial class adjustment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "StopTime",
                table: "PlatformJob",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "Exchange_ID_2",
                table: "LimitArbitrageStrategy4Settings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Exchange_ID_1",
                table: "LimitArbitrageStrategy4Settings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Exchange_2ID",
                table: "LimitArbitrageStrategy4Settings",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WSS_Url",
                table: "Exchanges",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DelayBetweenCallMilliseonds",
                table: "Exchanges",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApiUrl",
                table: "Exchanges",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_ID_1",
                table: "LimitArbitrageStrategy4Settings",
                column: "Exchange_ID_1",
                principalTable: "Exchanges",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_2ID",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropForeignKey(
                name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_ID_1",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropForeignKey(
                name: "FK_PlatformJob_AspNetUsers_User_ID",
                table: "PlatformJob");

            migrationBuilder.DropIndex(
                name: "IX_PlatformJob_User_ID",
                table: "PlatformJob");

            migrationBuilder.DropIndex(
                name: "IX_LimitArbitrageStrategy4Settings_Exchange_2ID",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropColumn(
                name: "User_ID",
                table: "PlatformJob");

            migrationBuilder.DropColumn(
                name: "Exchange_2ID",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StopTime",
                table: "PlatformJob",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Exchange_ID_2",
                table: "LimitArbitrageStrategy4Settings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Exchange_ID_1",
                table: "LimitArbitrageStrategy4Settings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "WSS_Url",
                table: "Exchanges",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "DelayBetweenCallMilliseonds",
                table: "Exchanges",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ApiUrl",
                table: "Exchanges",
                nullable: true,
                oldClrType: typeof(string));

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
    }
}
