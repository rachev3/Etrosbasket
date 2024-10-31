using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Etrosbasket.Data.Migrations
{
    /// <inheritdoc />
    public partial class Player_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneModels",
                table: "PhoneModels");

            migrationBuilder.RenameTable(
                name: "PhoneModels",
                newName: "Players");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "PhoneModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneModels",
                table: "PhoneModels",
                column: "Id");
        }
    }
}
