using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Etrosbasket.Data.Migrations
{
    /// <inheritdoc />
    public partial class Date_TeamAgainst_AddedInPlayerStatistic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PlayerStatistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TeamAgainst",
                table: "PlayerStatistics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "PlayerStatistics");

            migrationBuilder.DropColumn(
                name: "TeamAgainst",
                table: "PlayerStatistics");
        }
    }
}
