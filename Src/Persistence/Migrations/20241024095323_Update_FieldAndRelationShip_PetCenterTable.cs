using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Update_FieldAndRelationShip_PetCenterTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_PetCenter_AppUser_UserId",
            table: "PetCenter");

        migrationBuilder.DropTable(
            name: "JobSpecies");

        migrationBuilder.DropIndex(
            name: "IX_PetCenter_UserId",
            table: "PetCenter");

        migrationBuilder.DropIndex(
            name: "IX_Application_UserId",
            table: "Application");

        migrationBuilder.DropColumn(
            name: "HaveTransport",
            table: "PetCenter");

        migrationBuilder.DropColumn(
            name: "Location",
            table: "PetCenter");

        migrationBuilder.DropColumn(
            name: "Name",
            table: "PetCenter");

        migrationBuilder.DropColumn(
            name: "UserId",
            table: "PetCenter");

        migrationBuilder.DropColumn(
            name: "PhoneNumber",
            table: "Job");

        migrationBuilder.RenameColumn(
            name: "Image",
            table: "Application",
            newName: "Avatar");

        migrationBuilder.AddColumn<int>(
            name: "ApplicationId",
            table: "PetCenter",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "PetCenter",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "PetCenter",
            type: "datetime2(0)",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "PetCenterId",
            table: "AppUser",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.CreateTable(
            name: "SpeciesJob",
            columns: table => new
            {
                SpeciesId = table.Column<int>(type: "int", nullable: false),
                JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SpeciesJob", x => new { x.JobId, x.SpeciesId });
                table.ForeignKey(
                    name: "FK_SpeciesJob_Job_JobId",
                    column: x => x.JobId,
                    principalTable: "Job",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_SpeciesJob_Species_SpeciesId",
                    column: x => x.SpeciesId,
                    principalTable: "Species",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PetCenter_ApplicationId",
            table: "PetCenter",
            column: "ApplicationId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_AppUser_PetCenterId",
            table: "AppUser",
            column: "PetCenterId");

        migrationBuilder.CreateIndex(
            name: "IX_Application_UserId",
            table: "Application",
            column: "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_SpeciesJob_SpeciesId",
            table: "SpeciesJob",
            column: "SpeciesId");

        migrationBuilder.AddForeignKey(
            name: "FK_AppUser_PetCenter_PetCenterId",
            table: "AppUser",
            column: "PetCenterId",
            principalTable: "PetCenter",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_PetCenter_Application_ApplicationId",
            table: "PetCenter",
            column: "ApplicationId",
            principalTable: "Application",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AppUser_PetCenter_PetCenterId",
            table: "AppUser");

        migrationBuilder.DropForeignKey(
            name: "FK_PetCenter_Application_ApplicationId",
            table: "PetCenter");

        migrationBuilder.DropTable(
            name: "SpeciesJob");

        migrationBuilder.DropIndex(
            name: "IX_PetCenter_ApplicationId",
            table: "PetCenter");

        migrationBuilder.DropIndex(
            name: "IX_AppUser_PetCenterId",
            table: "AppUser");

        migrationBuilder.DropIndex(
            name: "IX_Application_UserId",
            table: "Application");

        migrationBuilder.DropColumn(
            name: "ApplicationId",
            table: "PetCenter");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "PetCenter");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "PetCenter");

        migrationBuilder.DropColumn(
            name: "PetCenterId",
            table: "AppUser");

        migrationBuilder.RenameColumn(
            name: "Avatar",
            table: "Application",
            newName: "Image");

        migrationBuilder.AddColumn<bool>(
            name: "HaveTransport",
            table: "PetCenter",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<string>(
            name: "Location",
            table: "PetCenter",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Name",
            table: "PetCenter",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<Guid>(
            name: "UserId",
            table: "PetCenter",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<string>(
            name: "PhoneNumber",
            table: "Job",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateTable(
            name: "JobSpecies",
            columns: table => new
            {
                JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SpeciesId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JobSpecies", x => new { x.JobId, x.SpeciesId });
                table.ForeignKey(
                    name: "FK_JobSpecies_Job_JobId",
                    column: x => x.JobId,
                    principalTable: "Job",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_JobSpecies_Species_SpeciesId",
                    column: x => x.SpeciesId,
                    principalTable: "Species",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PetCenter_UserId",
            table: "PetCenter",
            column: "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Application_UserId",
            table: "Application",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_JobSpecies_SpeciesId",
            table: "JobSpecies",
            column: "SpeciesId");

        migrationBuilder.AddForeignKey(
            name: "FK_PetCenter_AppUser_UserId",
            table: "PetCenter",
            column: "UserId",
            principalTable: "AppUser",
            principalColumn: "Id");
    }
}
