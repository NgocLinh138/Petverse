using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class AddJobTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "PetPhoto",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)");

        migrationBuilder.CreateTable(
            name: "Job",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetSitterServiceId = table.Column<int>(type: "int", nullable: false),
                Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                HasPhoto = table.Column<bool>(type: "bit", nullable: false),
                HasCamera = table.Column<bool>(type: "bit", nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                StartDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                EndDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Job", x => x.Id);
                table.ForeignKey(
                    name: "FK_Job_PetSitterService_PetSitterServiceId",
                    column: x => x.PetSitterServiceId,
                    principalTable: "PetSitterService",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Job_PetSitterServiceId",
            table: "Job",
            column: "PetSitterServiceId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Job");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "PetPhoto",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);
    }
}
