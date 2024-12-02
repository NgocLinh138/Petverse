using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Fix_RelationShip_Job_TimeTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_TimeTable_Job_JobId",
            table: "TimeTable");

        migrationBuilder.AddForeignKey(
            name: "FK_TimeTable_Job_JobId",
            table: "TimeTable",
            column: "JobId",
            principalTable: "Job",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_TimeTable_Job_JobId",
            table: "TimeTable");

        migrationBuilder.AddForeignKey(
            name: "FK_TimeTable_Job_JobId",
            table: "TimeTable",
            column: "JobId",
            principalTable: "Job",
            principalColumn: "Id");
    }
}
