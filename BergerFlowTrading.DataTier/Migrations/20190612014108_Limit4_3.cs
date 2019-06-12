using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BergerFlowTrading.DataTier.Migrations
{
    public partial class Limit4_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UserExchangeSecrets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "UserExchangeSecrets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "UserExchangeSecrets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "UserExchangeSecrets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LimitArbitrageStrategy4Settings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "LimitArbitrageStrategy4Settings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "LimitArbitrageStrategy4Settings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "LimitArbitrageStrategy4Settings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Exchanges",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Exchanges",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Exchanges",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Exchanges",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ExchangeCustomSettings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "ExchangeCustomSettings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ExchangeCustomSettings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "ExchangeCustomSettings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserExchangeSecrets");

            migrationBuilder.DropColumn(
                name: "CreatedTimeStamp",
                table: "UserExchangeSecrets");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserExchangeSecrets");

            migrationBuilder.DropColumn(
                name: "UpdatedTimeStamp",
                table: "UserExchangeSecrets");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropColumn(
                name: "CreatedTimeStamp",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropColumn(
                name: "UpdatedTimeStamp",
                table: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "CreatedTimeStamp",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "UpdatedTimeStamp",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ExchangeCustomSettings");

            migrationBuilder.DropColumn(
                name: "CreatedTimeStamp",
                table: "ExchangeCustomSettings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ExchangeCustomSettings");

            migrationBuilder.DropColumn(
                name: "UpdatedTimeStamp",
                table: "ExchangeCustomSettings");
        }
    }
}
