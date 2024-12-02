using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class ChangeNameField_Rating_To_Rate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Rating",
            table: "PetSitterService",
            newName: "Rate");

        migrationBuilder.RenameColumn(
            name: "Rating",
            table: "PetSitterRate",
            newName: "Rate");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Rate",
            table: "PetSitterService",
            newName: "Rating");

        migrationBuilder.RenameColumn(
            name: "Rate",
            table: "PetSitterRate",
            newName: "Rating");
    }
}
