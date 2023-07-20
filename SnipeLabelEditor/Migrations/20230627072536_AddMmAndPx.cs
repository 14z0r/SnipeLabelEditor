using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnipeLabelEditor.Migrations
{
    /// <inheritdoc />
    public partial class AddMmAndPx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Labels",
                newName: "WidthPx");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Labels",
                newName: "WidthMm");

            migrationBuilder.AddColumn<int>(
                name: "HeightMm",
                table: "Labels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HeightPx",
                table: "Labels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeightMm",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "HeightPx",
                table: "Labels");

            migrationBuilder.RenameColumn(
                name: "WidthPx",
                table: "Labels",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "WidthMm",
                table: "Labels",
                newName: "Height");
        }
    }
}
