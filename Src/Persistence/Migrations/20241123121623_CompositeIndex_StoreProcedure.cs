#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class CompositeIndex_StoreProcedure : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_PetId_VaccineId",
            table: "VaccineRecommendation",
            columns: new[] { "PetId", "VaccineId" });

        migrationBuilder.CreateIndex(
            name: "IX_Date_Id",
            table: "Schedule",
            columns: new[] { "Date", "Id" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_PetServiceId_Rate",
            table: "PetCenterService",
            columns: new[] { "PetServiceId", "Rate" },
            descending: new[] { false, true });

        migrationBuilder.CreateIndex(
            name: "IX_IsDeleted",
            table: "PetCenter",
            column: "IsDeleted");

        migrationBuilder.CreateIndex(
            name: "IX_CreatedDate",
            table: "AppUser",
            column: "CreatedDate");

        migrationBuilder.CreateIndex(
            name: "IX_CreatedDate",
            table: "Appointment",
            column: "CreatedDate");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_PetId_VaccineId",
            table: "VaccineRecommendation");

        migrationBuilder.DropIndex(
            name: "IX_Date_Id",
            table: "Schedule");

        migrationBuilder.DropIndex(
            name: "IX_PetServiceId_Rate",
            table: "PetCenterService");

        migrationBuilder.DropIndex(
            name: "IX_IsDeleted",
            table: "PetCenter");

        migrationBuilder.DropIndex(
            name: "IX_CreatedDate",
            table: "AppUser");

        migrationBuilder.DropIndex(
            name: "IX_CreatedDate",
            table: "Appointment");

        migrationBuilder.CreateIndex(
            name: "IX_VaccineRecommendation_PetId",
            table: "VaccineRecommendation",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_PetCenterService_PetServiceId",
            table: "PetCenterService",
            column: "PetServiceId");
    }
}
