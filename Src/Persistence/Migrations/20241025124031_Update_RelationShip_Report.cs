using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Update_RelationShip_Report : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Report_PetCenter_UserId",
            table: "Report");

        migrationBuilder.DropColumn(
            name: "PetCenterId",
            table: "Report");

        migrationBuilder.AddColumn<int>(
            name: "ApplicationId",
            table: "Report",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "Report",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "Report",
            type: "datetime2(0)",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_Report_ApplicationId",
            table: "Report",
            column: "ApplicationId");

        migrationBuilder.AddForeignKey(
            name: "FK_Report_Application_ApplicationId",
            table: "Report",
            column: "ApplicationId",
            principalTable: "Application",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Report_Application_ApplicationId",
            table: "Report");

        migrationBuilder.DropIndex(
            name: "IX_Report_ApplicationId",
            table: "Report");

        migrationBuilder.DropColumn(
            name: "ApplicationId",
            table: "Report");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "Report");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "Report");

        migrationBuilder.AddColumn<Guid>(
            name: "PetCenterId",
            table: "Report",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddForeignKey(
            name: "FK_Report_PetCenter_UserId",
            table: "Report",
            column: "UserId",
            principalTable: "PetCenter",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
