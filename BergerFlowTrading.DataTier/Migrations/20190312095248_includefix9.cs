using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BergerFlowTrading.DataTier.Migrations
{
    public partial class includefix9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    ApiUrl = table.Column<string>(nullable: true),
                    WSS_Url = table.Column<string>(nullable: true),
                    FacadeClassName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeCustomSettings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Exchange_ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeCustomSettings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExchangeCustomSettings_Exchanges_Exchange_ID",
                        column: x => x.Exchange_ID,
                        principalTable: "Exchanges",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LimitArbitrageStrategy4Settings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User_ID = table.Column<string>(nullable: false),
                    Exchange_ID_1 = table.Column<int>(nullable: false),
                    Symbol = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ManagementBalanceON = table.Column<bool>(nullable: false),
                    MinATRValue = table.Column<decimal>(type: "decimal(18, 10)", nullable: false),
                    USD_Value_To_Trade = table.Column<decimal>(type: "decimal(18, 10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LimitArbitrageStrategy4Settings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LimitArbitrageStrategy4Settings_Exchanges_Exchange_ID_1",
                        column: x => x.Exchange_ID_1,
                        principalTable: "Exchanges",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LimitArbitrageStrategy4Settings_AspNetUsers_User_ID",
                        column: x => x.User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserExchangeSecrets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User_ID = table.Column<string>(nullable: false),
                    Exchange_ID = table.Column<int>(nullable: false),
                    Api_ID = table.Column<string>(nullable: false),
                    Api_Secret = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExchangeSecrets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserExchangeSecrets_Exchanges_Exchange_ID",
                        column: x => x.Exchange_ID,
                        principalTable: "Exchanges",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserExchangeSecrets_AspNetUsers_User_ID",
                        column: x => x.User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeCustomSettings_Exchange_ID",
                table: "ExchangeCustomSettings",
                column: "Exchange_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LimitArbitrageStrategy4Settings_Exchange_ID_1",
                table: "LimitArbitrageStrategy4Settings",
                column: "Exchange_ID_1");

            migrationBuilder.CreateIndex(
                name: "IX_LimitArbitrageStrategy4Settings_User_ID",
                table: "LimitArbitrageStrategy4Settings",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserExchangeSecrets_Exchange_ID",
                table: "UserExchangeSecrets",
                column: "Exchange_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserExchangeSecrets_User_ID",
                table: "UserExchangeSecrets",
                column: "User_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeCustomSettings");

            migrationBuilder.DropTable(
                name: "LimitArbitrageStrategy4Settings");

            migrationBuilder.DropTable(
                name: "UserExchangeSecrets");

            migrationBuilder.DropTable(
                name: "Exchanges");
        }
    }
}
