using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewField_Application_Appointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PetId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Application",
                type: "datetime2(0)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Application",
                type: "datetime2(0)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PetId",
                table: "Appointment",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Pet_PetId",
                table: "Appointment",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Pet_PetId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_PetId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Application");
        }
    }
}
