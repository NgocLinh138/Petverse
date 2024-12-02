using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Add_AppointmentTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Appointment",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetSitterServiceId = table.Column<int>(type: "int", nullable: false),
                StartTime = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                EndTime = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Appointment", x => x.Id);
                table.ForeignKey(
                    name: "FK_Appointment_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Appointment_PetSitterService_PetSitterServiceId",
                    column: x => x.PetSitterServiceId,
                    principalTable: "PetSitterService",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Appointment_PetSitterServiceId",
            table: "Appointment",
            column: "PetSitterServiceId");

        migrationBuilder.CreateIndex(
            name: "IX_Appointment_UserId",
            table: "Appointment",
            column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Appointment");
    }
}
