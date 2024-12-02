using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class ChangeDataType_PetTypeTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "PetType",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "int");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "Name",
            table: "PetType",
            type: "int",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");
    }
}
