using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Update_WorkHistory_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "HaveTransport",
            table: "WorkHistory",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<string>(
            name: "Location",
            table: "WorkHistory",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<int>(
            name: "NumPet",
            table: "WorkHistory",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "HaveTransport",
            table: "WorkHistory");

        migrationBuilder.DropColumn(
            name: "Location",
            table: "WorkHistory");

        migrationBuilder.DropColumn(
            name: "NumPet",
            table: "WorkHistory");
    }
}
