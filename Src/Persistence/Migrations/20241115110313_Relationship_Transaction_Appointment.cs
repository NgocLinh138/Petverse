using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Relationship_Transaction_Appointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Appointment_AppointmentId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_AppointmentId",
                table: "Transaction");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AppointmentId",
                table: "Transaction",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Appointment_AppointmentId",
                table: "Transaction",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Appointment_AppointmentId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_AppointmentId",
                table: "Transaction");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AppointmentId",
                table: "Transaction",
                column: "AppointmentId",
                unique: true,
                filter: "[AppointmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Appointment_AppointmentId",
                table: "Transaction",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "Id");
        }
    }
}
