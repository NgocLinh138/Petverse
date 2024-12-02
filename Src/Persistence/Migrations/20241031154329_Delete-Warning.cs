using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class DeleteWarning : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Warning");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Warning",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetId = table.Column<int>(type: "int", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Warning", x => x.Id);
                table.ForeignKey(
                    name: "FK_Warning_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Warning_Pet_PetId",
                    column: x => x.PetId,
                    principalTable: "Pet",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Warning_PetId",
            table: "Warning",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_Warning_UserId",
            table: "Warning",
            column: "UserId");
    }
}
