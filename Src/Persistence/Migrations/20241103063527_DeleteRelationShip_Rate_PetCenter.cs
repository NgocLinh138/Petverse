using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class DeleteRelationShip_Rate_PetCenter : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_PetCenterRate_PetCenterService_PetCenterServiceId",
            table: "PetCenterRate");

        migrationBuilder.DropIndex(
            name: "IX_PetCenterRate_PetCenterServiceId",
            table: "PetCenterRate");

        migrationBuilder.DropColumn(
            name: "PetCenterServiceId",
            table: "PetCenterRate");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "PetCenterServiceId",
            table: "PetCenterRate",
            type: "int",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_PetCenterRate_PetCenterServiceId",
            table: "PetCenterRate",
            column: "PetCenterServiceId");

        migrationBuilder.AddForeignKey(
            name: "FK_PetCenterRate_PetCenterService_PetCenterServiceId",
            table: "PetCenterRate",
            column: "PetCenterServiceId",
            principalTable: "PetCenterService",
            principalColumn: "Id");
    }
}
