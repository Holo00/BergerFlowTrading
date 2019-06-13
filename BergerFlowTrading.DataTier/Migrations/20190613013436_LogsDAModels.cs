using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BergerFlowTrading.DataTier.Migrations
{
    public partial class LogsDAModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlatformJob",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    StopTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformJob", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeLogs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Class = table.Column<string>(nullable: false),
                    LineNumber = table.Column<int>(nullable: false),
                    eventType = table.Column<int>(nullable: false),
                    DetailLevelKeyword = table.Column<string>(nullable: true),
                    ExceptionMessage = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true),
                    InnerException = table.Column<string>(nullable: true),
                    PlatformJob_ID = table.Column<int>(nullable: false),
                    Exchange_ID = table.Column<int>(nullable: false),
                    Symbol = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeLogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExchangeLogs_Exchanges_Exchange_ID",
                        column: x => x.Exchange_ID,
                        principalTable: "Exchanges",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExchangeLogs_PlatformJob_PlatformJob_ID",
                        column: x => x.PlatformJob_ID,
                        principalTable: "PlatformJob",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlatformLogs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Class = table.Column<string>(nullable: false),
                    LineNumber = table.Column<int>(nullable: false),
                    eventType = table.Column<int>(nullable: false),
                    DetailLevelKeyword = table.Column<string>(nullable: true),
                    ExceptionMessage = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true),
                    InnerException = table.Column<string>(nullable: true),
                    PlatformJob_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformLogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlatformLogs_PlatformJob_PlatformJob_ID",
                        column: x => x.PlatformJob_ID,
                        principalTable: "PlatformJob",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StrategyRuns",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    StopTime = table.Column<DateTime>(nullable: false),
                    StrategyDescription = table.Column<string>(nullable: false),
                    StrategyType = table.Column<int>(nullable: false),
                    StrategySettings_ID = table.Column<int>(nullable: false),
                    PlatformJob_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrategyRuns", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StrategyRuns_PlatformJob_PlatformJob_ID",
                        column: x => x.PlatformJob_ID,
                        principalTable: "PlatformJob",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StrategyLog",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Class = table.Column<string>(nullable: false),
                    LineNumber = table.Column<int>(nullable: false),
                    eventType = table.Column<int>(nullable: false),
                    DetailLevelKeyword = table.Column<string>(nullable: true),
                    ExceptionMessage = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true),
                    InnerException = table.Column<string>(nullable: true),
                    StrategyRuns_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrategyLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StrategyLog_StrategyRuns_StrategyRuns_ID",
                        column: x => x.StrategyRuns_ID,
                        principalTable: "StrategyRuns",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeLogs_Exchange_ID",
                table: "ExchangeLogs",
                column: "Exchange_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeLogs_PlatformJob_ID",
                table: "ExchangeLogs",
                column: "PlatformJob_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformLogs_PlatformJob_ID",
                table: "PlatformLogs",
                column: "PlatformJob_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StrategyLog_StrategyRuns_ID",
                table: "StrategyLog",
                column: "StrategyRuns_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StrategyRuns_PlatformJob_ID",
                table: "StrategyRuns",
                column: "PlatformJob_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeLogs");

            migrationBuilder.DropTable(
                name: "PlatformLogs");

            migrationBuilder.DropTable(
                name: "StrategyLog");

            migrationBuilder.DropTable(
                name: "StrategyRuns");

            migrationBuilder.DropTable(
                name: "PlatformJob");
        }
    }
}
