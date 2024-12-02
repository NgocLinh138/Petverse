using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class DeleteAllDoctor : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "DoctorAppointment");

        migrationBuilder.DropTable(
            name: "DoctorRate");

        migrationBuilder.DropTable(
            name: "Doctor");

        migrationBuilder.DropColumn(
            name: "Type",
            table: "Application");

        migrationBuilder.AddColumn<string>(
            name: "CancelReason",
            table: "Appointment",
            type: "nvarchar(max)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CancelReason",
            table: "Appointment");

        migrationBuilder.AddColumn<int>(
            name: "Type",
            table: "Application",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "Doctor",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ApplicationId = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                IsVerified = table.Column<bool>(type: "bit", nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Doctor", x => x.Id);
                table.ForeignKey(
                    name: "FK_Doctor_Application_ApplicationId",
                    column: x => x.ApplicationId,
                    principalTable: "Application",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "DoctorAppointment",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DoctorAppointment", x => x.Id);
                table.ForeignKey(
                    name: "FK_DoctorAppointment_AppUser_AppUserId",
                    column: x => x.AppUserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_DoctorAppointment_Appointment_Id",
                    column: x => x.Id,
                    principalTable: "Appointment",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_DoctorAppointment_Doctor_DoctorId",
                    column: x => x.DoctorId,
                    principalTable: "Doctor",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "DoctorRate",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                Rate = table.Column<float>(type: "real", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DoctorRate", x => x.Id);
                table.ForeignKey(
                    name: "FK_DoctorRate_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_DoctorRate_Doctor_DoctorId",
                    column: x => x.DoctorId,
                    principalTable: "Doctor",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Doctor_ApplicationId",
            table: "Doctor",
            column: "ApplicationId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_DoctorAppointment_AppUserId",
            table: "DoctorAppointment",
            column: "AppUserId");

        migrationBuilder.CreateIndex(
            name: "IX_DoctorAppointment_DoctorId",
            table: "DoctorAppointment",
            column: "DoctorId");

        migrationBuilder.CreateIndex(
            name: "IX_DoctorRate_DoctorId",
            table: "DoctorRate",
            column: "DoctorId");

        migrationBuilder.CreateIndex(
            name: "IX_DoctorRate_UserId",
            table: "DoctorRate",
            column: "UserId");
    }
}
