using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class MakeRelationShip_Job_PetSitter : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Job_PetSitterService_PetSitterServiceId",
            table: "Job");

        migrationBuilder.DropIndex(
            name: "IX_Job_PetSitterServiceId",
            table: "Job");

        migrationBuilder.DropColumn(
            name: "PetSitterServiceId",
            table: "Job");

        migrationBuilder.AddColumn<Guid>(
            name: "PetSitterId",
            table: "Job",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.CreateIndex(
            name: "IX_Job_PetSitterId",
            table: "Job",
            column: "PetSitterId",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_Job_PetSitter_PetSitterId",
            table: "Job",
            column: "PetSitterId",
            principalTable: "PetSitter",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Job_PetSitter_PetSitterId",
            table: "Job");

        migrationBuilder.DropIndex(
            name: "IX_Job_PetSitterId",
            table: "Job");

        migrationBuilder.DropColumn(
            name: "PetSitterId",
            table: "Job");

        migrationBuilder.AddColumn<int>(
            name: "PetSitterServiceId",
            table: "Job",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateIndex(
            name: "IX_Job_PetSitterServiceId",
            table: "Job",
            column: "PetSitterServiceId",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_Job_PetSitterService_PetSitterServiceId",
            table: "Job",
            column: "PetSitterServiceId",
            principalTable: "PetSitterService",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
