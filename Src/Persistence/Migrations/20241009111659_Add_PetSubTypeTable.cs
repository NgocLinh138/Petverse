using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Add_PetSubTypeTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "PetSubType",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetTypeId = table.Column<int>(type: "int", nullable: false),
                SubName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetSubType", x => x.Id);
                table.ForeignKey(
                    name: "FK_PetSubType_PetType_PetTypeId",
                    column: x => x.PetTypeId,
                    principalTable: "PetType",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PetSubType_PetTypeId",
            table: "PetSubType",
            column: "PetTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_PetSubType_SubName",
            table: "PetSubType",
            column: "SubName",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PetSubType");
    }
}
