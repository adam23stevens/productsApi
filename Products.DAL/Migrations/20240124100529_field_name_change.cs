using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.DAL.Migrations
{
    /// <inheritdoc />
    public partial class field_name_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ColourName",
                table: "Colours",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Colours",
                newName: "ColourName");
        }
    }
}
