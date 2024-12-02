using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class PetCenterNumReport : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "NumReported",
            table: "AppUser");

        migrationBuilder.AddColumn<int>(
            name: "NumReported",
            table: "PetCenter",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "NumReported",
            table: "PetCenter");

        migrationBuilder.AddColumn<int>(
            name: "NumReported",
            table: "AppUser",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }
}
