using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Etrosbasket.Data.Migrations
{
    /// <inheritdoc />
    public partial class PlayerStatistic_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Players",
                newName: "PlayerId");

            migrationBuilder.CreateTable(
                name: "PlayerStatistics",
                columns: table => new
                {
                    StatisticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Minutes = table.Column<TimeSpan>(type: "time", nullable: false),
                    TwoPoints_Made = table.Column<int>(type: "int", nullable: false),
                    TwoPoints_Attempted = table.Column<int>(type: "int", nullable: false),
                    ThreePoints_Made = table.Column<int>(type: "int", nullable: false),
                    ThreePoints_Attempted = table.Column<int>(type: "int", nullable: false),
                    FreeThrows_Made = table.Column<int>(type: "int", nullable: false),
                    FreeThrows_Attempted = table.Column<int>(type: "int", nullable: false),
                    OffensiveRebounds = table.Column<int>(type: "int", nullable: false),
                    DeffensiveRebounds = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    Turnovers = table.Column<int>(type: "int", nullable: false),
                    Steals = table.Column<int>(type: "int", nullable: false),
                    Blocks = table.Column<int>(type: "int", nullable: false),
                    PersonalFaul = table.Column<int>(type: "int", nullable: false),
                    FaulDrawned = table.Column<int>(type: "int", nullable: false),
                    PlusMinus = table.Column<int>(type: "int", nullable: false),
                    Efficiency = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatistics", x => x.StatisticId);
                    table.ForeignKey(
                        name: "FK_PlayerStatistics_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerStatistics");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Players",
                newName: "Id");
        }
    }
}
