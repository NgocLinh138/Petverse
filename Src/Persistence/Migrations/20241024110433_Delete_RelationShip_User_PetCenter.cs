using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Delete_RelationShip_User_PetCenter : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AppUser_PetCenter_PetCenterId",
            table: "AppUser");

        migrationBuilder.DropIndex(
            name: "IX_AppUser_PetCenterId",
            table: "AppUser");

        migrationBuilder.DropColumn(
            name: "PetCenterId",
            table: "AppUser");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "PetCenterId",
            table: "AppUser",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.CreateIndex(
            name: "IX_AppUser_PetCenterId",
            table: "AppUser",
            column: "PetCenterId");

        migrationBuilder.AddForeignKey(
            name: "FK_AppUser_PetCenter_PetCenterId",
            table: "AppUser",
            column: "PetCenterId",
            principalTable: "PetCenter",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
