using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class AddTimeTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "EndDate",
            table: "Job");

        migrationBuilder.DropColumn(
            name: "StartDate",
            table: "Job");

        migrationBuilder.RenameColumn(
            name: "HasPhoto",
            table: "Job",
            newName: "HaveTransport");

        migrationBuilder.RenameColumn(
            name: "HasCamera",
            table: "Job",
            newName: "HavePhoto");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "PetType",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AddColumn<bool>(
            name: "HaveCamera",
            table: "Job",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.CreateTable(
            name: "TimeTable",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TimeTable", x => x.Id);
                table.ForeignKey(
                    name: "FK_TimeTable_Job_JobId",
                    column: x => x.JobId,
                    principalTable: "Job",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_PetType_Name",
            table: "PetType",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_TimeTable_JobId",
            table: "TimeTable",
            column: "JobId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TimeTable");

        migrationBuilder.DropIndex(
            name: "IX_PetType_Name",
            table: "PetType");

        migrationBuilder.DropColumn(
            name: "HaveCamera",
            table: "Job");

        migrationBuilder.RenameColumn(
            name: "HaveTransport",
            table: "Job",
            newName: "HasPhoto");

        migrationBuilder.RenameColumn(
            name: "HavePhoto",
            table: "Job",
            newName: "HasCamera");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "PetType",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");

        migrationBuilder.AddColumn<DateTime>(
            name: "EndDate",
            table: "Job",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "StartDate",
            table: "Job",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }
}
