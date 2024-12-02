using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Update_Pet_RelationShip_v2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Breed_PetType_PetTypeId",
            table: "Breed");

        migrationBuilder.DropForeignKey(
            name: "FK_Pet_PetSubType_PetSubTypeId",
            table: "Pet");

        migrationBuilder.DropForeignKey(
            name: "FK_PetSitterServiceType_PetSitterService_PetSitterServiceId",
            table: "PetSitterServiceType");

        migrationBuilder.DropForeignKey(
            name: "FK_PetSitterServiceType_PetType_PetTypeId",
            table: "PetSitterServiceType");

        migrationBuilder.DropForeignKey(
            name: "FK_PetSubType_PetType_PetTypeId",
            table: "PetSubType");

        migrationBuilder.AddForeignKey(
            name: "FK_Breed_PetType_PetTypeId",
            table: "Breed",
            column: "PetTypeId",
            principalTable: "PetType",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Pet_PetSubType_PetSubTypeId",
            table: "Pet",
            column: "PetSubTypeId",
            principalTable: "PetSubType",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_PetSitterServiceType_PetSitterService_PetSitterServiceId",
            table: "PetSitterServiceType",
            column: "PetSitterServiceId",
            principalTable: "PetSitterService",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_PetSitterServiceType_PetType_PetTypeId",
            table: "PetSitterServiceType",
            column: "PetTypeId",
            principalTable: "PetType",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_PetSubType_PetType_PetTypeId",
            table: "PetSubType",
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

        migrationBuilder.DropForeignKey(
            name: "FK_Pet_PetSubType_PetSubTypeId",
            table: "Pet");

        migrationBuilder.DropForeignKey(
            name: "FK_PetSitterServiceType_PetSitterService_PetSitterServiceId",
            table: "PetSitterServiceType");

        migrationBuilder.DropForeignKey(
            name: "FK_PetSitterServiceType_PetType_PetTypeId",
            table: "PetSitterServiceType");

        migrationBuilder.DropForeignKey(
            name: "FK_PetSubType_PetType_PetTypeId",
            table: "PetSubType");

        migrationBuilder.AddForeignKey(
            name: "FK_Breed_PetType_PetTypeId",
            table: "Breed",
            column: "PetTypeId",
            principalTable: "PetType",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Pet_PetSubType_PetSubTypeId",
            table: "Pet",
            column: "PetSubTypeId",
            principalTable: "PetSubType",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_PetSitterServiceType_PetSitterService_PetSitterServiceId",
            table: "PetSitterServiceType",
            column: "PetSitterServiceId",
            principalTable: "PetSitterService",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_PetSitterServiceType_PetType_PetTypeId",
            table: "PetSitterServiceType",
            column: "PetTypeId",
            principalTable: "PetType",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_PetSubType_PetType_PetTypeId",
            table: "PetSubType",
            column: "PetTypeId",
            principalTable: "PetType",
            principalColumn: "Id");
    }
}
