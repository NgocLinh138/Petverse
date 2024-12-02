using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class ChangeNameTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PetCenterRate");

        migrationBuilder.DropTable(
            name: "Photo");

        migrationBuilder.CreateTable(
            name: "AppointmentRate",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Rate = table.Column<float>(type: "real", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                DeletedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppointmentRate", x => x.Id);
                table.ForeignKey(
                    name: "FK_AppointmentRate_Appointment_AppointmentId",
                    column: x => x.AppointmentId,
                    principalTable: "Appointment",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PetImage",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetId = table.Column<int>(type: "int", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetImage", x => x.Id);
                table.ForeignKey(
                    name: "FK_PetImage_Pet_PetId",
                    column: x => x.PetId,
                    principalTable: "Pet",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AppointmentRate_AppointmentId",
            table: "AppointmentRate",
            column: "AppointmentId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_PetImage_PetId",
            table: "PetImage",
            column: "PetId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AppointmentRate");

        migrationBuilder.DropTable(
            name: "PetImage");

        migrationBuilder.CreateTable(
            name: "PetCenterRate",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                Rate = table.Column<float>(type: "real", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetCenterRate", x => x.Id);
                table.ForeignKey(
                    name: "FK_PetCenterRate_Appointment_AppointmentId",
                    column: x => x.AppointmentId,
                    principalTable: "Appointment",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Photo",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetId = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Photo", x => x.Id);
                table.ForeignKey(
                    name: "FK_Photo_Pet_PetId",
                    column: x => x.PetId,
                    principalTable: "Pet",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PetCenterRate_AppointmentId",
            table: "PetCenterRate",
            column: "AppointmentId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Photo_PetId",
            table: "Photo",
            column: "PetId");
    }
}
