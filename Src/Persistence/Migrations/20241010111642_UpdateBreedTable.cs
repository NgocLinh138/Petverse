using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class UpdateBreedTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "PetType",
            table: "Breed",
            newName: "PetTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_Breed_PetTypeId",
            table: "Breed",
            column: "PetTypeId");

        migrationBuilder.AddForeignKey(
            name: "FK_Breed_PetType_PetTypeId",
            table: "Breed",
            column: "PetTypeId",
            principalTable: "PetType",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Breed_PetType_PetTypeId",
            table: "Breed");

        migrationBuilder.DropIndex(
            name: "IX_Breed_PetTypeId",
            table: "Breed");

        migrationBuilder.RenameColumn(
            name: "PetTypeId",
            table: "Breed",
            newName: "PetType");
    }
}
