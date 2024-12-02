using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Add_CertificationTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Certification",
            table: "Application");

        migrationBuilder.CreateTable(
            name: "Certification",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ApplicationId = table.Column<int>(type: "int", nullable: false),
                Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Certification", x => x.Id);
                table.ForeignKey(
                    name: "FK_Certification_Application_ApplicationId",
                    column: x => x.ApplicationId,
                    principalTable: "Application",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Certification_ApplicationId",
            table: "Certification",
            column: "ApplicationId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Certification");

        migrationBuilder.AddColumn<string>(
            name: "Certification",
            table: "Application",
            type: "nvarchar(max)",
            nullable: true);
    }
}
