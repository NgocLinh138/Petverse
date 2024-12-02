using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class ChangeFieldName_ApplicationTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DateOfBirth",
            table: "Application");

        migrationBuilder.RenameColumn(
            name: "FullName",
            table: "Application",
            newName: "Name");

        migrationBuilder.RenameColumn(
            name: "Avatar",
            table: "Application",
            newName: "Image");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Name",
            table: "Application",
            newName: "FullName");

        migrationBuilder.RenameColumn(
            name: "Image",
            table: "Application",
            newName: "Avatar");

        migrationBuilder.AddColumn<DateTime>(
            name: "DateOfBirth",
            table: "Application",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }
}
