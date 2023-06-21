using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnipeLabelEditor.Migrations
{
    /// <inheritdoc />
    public partial class LabelAddHeightAndWidth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Labels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Labels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Labels");
        }
    }
}
