using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class UpdateAfterReview3 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "IsFree",
            table: "Place",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<int>(
            name: "Capacity",
            table: "PetCenterService",
            type: "int",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Schedule",
            table: "PetCenterService",
            type: "nvarchar(max)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "IsFree",
            table: "Place");

        migrationBuilder.DropColumn(
            name: "Capacity",
            table: "PetCenterService");

        migrationBuilder.DropColumn(
            name: "Schedule",
            table: "PetCenterService");
    }
}
