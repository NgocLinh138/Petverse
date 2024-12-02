using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class AddNewFields_PetSitter_Application : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "IsVerified",
            table: "PetSitter",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<int>(
            name: "Yoe",
            table: "PetSitter",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "Yoe",
            table: "Application",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "IsVerified",
            table: "PetSitter");

        migrationBuilder.DropColumn(
            name: "Yoe",
            table: "PetSitter");

        migrationBuilder.DropColumn(
            name: "Yoe",
            table: "Application");
    }
}
