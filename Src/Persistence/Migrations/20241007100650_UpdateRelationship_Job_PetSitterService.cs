using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class UpdateRelationship_Job_PetSitterService : Migration
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

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Job_PetSitterService_PetSitterServiceId",
            table: "Job");

        migrationBuilder.DropIndex(
            name: "IX_Job_PetSitterServiceId",
            table: "Job");

        migrationBuilder.CreateIndex(
            name: "IX_Job_PetSitterServiceId",
            table: "Job",
            column: "PetSitterServiceId");

        migrationBuilder.AddForeignKey(
            name: "FK_Job_PetSitterService_PetSitterServiceId",
            table: "Job",
            column: "PetSitterServiceId",
            principalTable: "PetSitterService",
            principalColumn: "Id");
    }
}
