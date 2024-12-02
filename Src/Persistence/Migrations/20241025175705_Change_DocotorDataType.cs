using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Change_DocotorDataType : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "StartTime",
            table: "Doctor",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "EndTime",
            table: "Doctor",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "StartTime",
            table: "Doctor",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "EndTime",
            table: "Doctor",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);
    }
}
