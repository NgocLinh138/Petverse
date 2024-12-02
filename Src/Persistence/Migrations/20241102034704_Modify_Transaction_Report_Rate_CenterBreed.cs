using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Modify_Transaction_Report_Rate_CenterBreed : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Report_Application_ApplicationId",
            table: "Report");

        migrationBuilder.DropTable(
            name: "Payment");

        migrationBuilder.DropIndex(
            name: "IX_Report_ApplicationId",
            table: "Report");

        migrationBuilder.DropColumn(
            name: "ApplicationId",
            table: "Report");

        migrationBuilder.AddColumn<Guid>(
            name: "AppointmentId",
            table: "Report",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<Guid>(
            name: "AppointmentId",
            table: "PetCenterRate",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<string>(
            name: "Image",
            table: "CenterBreed",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<int>(
            name: "Status",
            table: "CenterBreed",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<decimal>(
            name: "Balance",
            table: "AppUser",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "Appointment",
            type: "datetime2(0)",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "Transaction",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                OrderCode = table.Column<int>(type: "int", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsMinus = table.Column<bool>(type: "bit", nullable: false),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Transaction", x => x.Id);
                table.ForeignKey(
                    name: "FK_Transaction_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Transaction_Appointment_AppointmentId",
                    column: x => x.AppointmentId,
                    principalTable: "Appointment",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Report_AppointmentId",
            table: "Report",
            column: "AppointmentId");

        migrationBuilder.CreateIndex(
            name: "IX_PetCenterRate_AppointmentId",
            table: "PetCenterRate",
            column: "AppointmentId");

        migrationBuilder.CreateIndex(
            name: "IX_Transaction_AppointmentId",
            table: "Transaction",
            column: "AppointmentId",
            unique: true,
            filter: "[AppointmentId] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_Transaction_UserId",
            table: "Transaction",
            column: "UserId");

        migrationBuilder.AddForeignKey(
            name: "FK_PetCenterRate_Appointment_AppointmentId",
            table: "PetCenterRate",
            column: "AppointmentId",
            principalTable: "Appointment",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Report_Appointment_AppointmentId",
            table: "Report",
            column: "AppointmentId",
            principalTable: "Appointment",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_PetCenterRate_Appointment_AppointmentId",
            table: "PetCenterRate");

        migrationBuilder.DropForeignKey(
            name: "FK_Report_Appointment_AppointmentId",
            table: "Report");

        migrationBuilder.DropTable(
            name: "Transaction");

        migrationBuilder.DropIndex(
            name: "IX_Report_AppointmentId",
            table: "Report");

        migrationBuilder.DropIndex(
            name: "IX_PetCenterRate_AppointmentId",
            table: "PetCenterRate");

        migrationBuilder.DropColumn(
            name: "AppointmentId",
            table: "Report");

        migrationBuilder.DropColumn(
            name: "AppointmentId",
            table: "PetCenterRate");

        migrationBuilder.DropColumn(
            name: "Image",
            table: "CenterBreed");

        migrationBuilder.DropColumn(
            name: "Status",
            table: "CenterBreed");

        migrationBuilder.DropColumn(
            name: "Balance",
            table: "AppUser");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "Appointment");

        migrationBuilder.AddColumn<int>(
            name: "ApplicationId",
            table: "Report",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "Payment",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                OrderCode = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Payment", x => x.Id);
                table.ForeignKey(
                    name: "FK_Payment_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Payment_Appointment_AppointmentId",
                    column: x => x.AppointmentId,
                    principalTable: "Appointment",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Report_ApplicationId",
            table: "Report",
            column: "ApplicationId");

        migrationBuilder.CreateIndex(
            name: "IX_Payment_AppointmentId",
            table: "Payment",
            column: "AppointmentId");

        migrationBuilder.CreateIndex(
            name: "IX_Payment_UserId",
            table: "Payment",
            column: "UserId");

        migrationBuilder.AddForeignKey(
            name: "FK_Report_Application_ApplicationId",
            table: "Report",
            column: "ApplicationId",
            principalTable: "Application",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
