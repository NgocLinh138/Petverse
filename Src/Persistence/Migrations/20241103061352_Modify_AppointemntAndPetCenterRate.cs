using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Modify_AppointemntAndPetCenterRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetCenterRate_PetCenterService_PetCenterServiceId",
                table: "PetCenterRate");

            migrationBuilder.DropIndex(
                name: "IX_Report_AppointmentId",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_PetCenterRate_AppointmentId",
                table: "PetCenterRate");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Report",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PetCenterServiceId",
                table: "PetCenterRate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "PetCenterId",
                table: "Appointment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Report_AppointmentId",
                table: "Report",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PetCenterRate_AppointmentId",
                table: "PetCenterRate",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PetCenterId",
                table: "Appointment",
                column: "PetCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_PetCenter_PetCenterId",
                table: "Appointment",
                column: "PetCenterId",
                principalTable: "PetCenter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetCenterRate_PetCenterService_PetCenterServiceId",
                table: "PetCenterRate",
                column: "PetCenterServiceId",
                principalTable: "PetCenterService",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_PetCenter_PetCenterId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_PetCenterRate_PetCenterService_PetCenterServiceId",
                table: "PetCenterRate");

            migrationBuilder.DropIndex(
                name: "IX_Report_AppointmentId",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_PetCenterRate_AppointmentId",
                table: "PetCenterRate");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_PetCenterId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PetCenterId",
                table: "Appointment");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Report",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PetCenterServiceId",
                table: "PetCenterRate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Report_AppointmentId",
                table: "Report",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PetCenterRate_AppointmentId",
                table: "PetCenterRate",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetCenterRate_PetCenterService_PetCenterServiceId",
                table: "PetCenterRate",
                column: "PetCenterServiceId",
                principalTable: "PetCenterService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
