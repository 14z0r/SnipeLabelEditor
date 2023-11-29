using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnipeLabelEditor.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoriteField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "Labels",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "Labels");
        }
    }
}
