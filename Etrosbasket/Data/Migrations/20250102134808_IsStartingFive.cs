using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Etrosbasket.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsStartingFive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStartingFive",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStartingFive",
                table: "Players");
        }
    }
}
