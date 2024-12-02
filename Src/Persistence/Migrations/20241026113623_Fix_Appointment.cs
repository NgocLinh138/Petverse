using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Fix_Appointment : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_BreedAppointment_CenterBreed_PetId",
            table: "BreedAppointment");

        migrationBuilder.DropForeignKey(
            name: "FK_BreedAppointment_Pet_PetId",
            table: "BreedAppointment");

        migrationBuilder.DropForeignKey(
            name: "FK_ServiceAppointment_Pet_PetId",
            table: "ServiceAppointment");

        migrationBuilder.DropIndex(
            name: "IX_ServiceAppointment_PetId",
            table: "ServiceAppointment");

        migrationBuilder.DropIndex(
            name: "IX_BreedAppointment_PetId",
            table: "BreedAppointment");

        migrationBuilder.DropColumn(
            name: "PetId",
            table: "ServiceAppointment");

        migrationBuilder.DropColumn(
            name: "PetId",
            table: "BreedAppointment");

        migrationBuilder.AddColumn<int>(
            name: "PetId",
            table: "Appointment",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateIndex(
            name: "IX_BreedAppointment_CenterBreedId",
            table: "BreedAppointment",
            column: "CenterBreedId");

        migrationBuilder.CreateIndex(
            name: "IX_Appointment_PetId",
            table: "Appointment",
            column: "PetId");

        migrationBuilder.AddForeignKey(
            name: "FK_Appointment_Pet_PetId",
            table: "Appointment",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_BreedAppointment_CenterBreed_CenterBreedId",
            table: "BreedAppointment",
            column: "CenterBreedId",
            principalTable: "CenterBreed",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Appointment_Pet_PetId",
            table: "Appointment");

        migrationBuilder.DropForeignKey(
            name: "FK_BreedAppointment_CenterBreed_CenterBreedId",
            table: "BreedAppointment");

        migrationBuilder.DropIndex(
            name: "IX_BreedAppointment_CenterBreedId",
            table: "BreedAppointment");

        migrationBuilder.DropIndex(
            name: "IX_Appointment_PetId",
            table: "Appointment");

        migrationBuilder.DropColumn(
            name: "PetId",
            table: "Appointment");

        migrationBuilder.AddColumn<int>(
            name: "PetId",
            table: "ServiceAppointment",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "PetId",
            table: "BreedAppointment",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateIndex(
            name: "IX_ServiceAppointment_PetId",
            table: "ServiceAppointment",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_BreedAppointment_PetId",
            table: "BreedAppointment",
            column: "PetId");

        migrationBuilder.AddForeignKey(
            name: "FK_BreedAppointment_CenterBreed_PetId",
            table: "BreedAppointment",
            column: "PetId",
            principalTable: "CenterBreed",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_BreedAppointment_Pet_PetId",
            table: "BreedAppointment",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_ServiceAppointment_Pet_PetId",
            table: "ServiceAppointment",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
