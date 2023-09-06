using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnipeLabelEditor.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageBaseString",
                table: "Labels",
                newName: "PdfBaseString");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PdfBaseString",
                table: "Labels",
                newName: "ImageBaseString");
        }
    }
}
