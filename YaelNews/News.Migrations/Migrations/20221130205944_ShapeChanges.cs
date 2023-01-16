using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class ShapeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Shapes",
                newName: "Name2");

            migrationBuilder.AlterColumn<int>(
                name: "Color",
                table: "Shapes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name2",
                table: "Shapes",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Shapes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
