using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class DateTrackingCenterBreed : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "CenterBreed",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "CenterBreed",
            type: "datetime2(0)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "CenterBreed");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "CenterBreed");
    }
}
