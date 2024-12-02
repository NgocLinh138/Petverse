using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Add_PetTypeTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "PetSitter",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "PetSitter",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.CreateTable(
            name: "PetType",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetType", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PetSitterServiceType",
            columns: table => new
            {
                PetTypeId = table.Column<int>(type: "int", nullable: false),
                PetSitterServiceId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetSitterServiceType", x => new { x.PetSitterServiceId, x.PetTypeId });
                table.ForeignKey(
                    name: "FK_PetSitterServiceType_PetSitterService_PetSitterServiceId",
                    column: x => x.PetSitterServiceId,
                    principalTable: "PetSitterService",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PetSitterServiceType_PetType_PetTypeId",
                    column: x => x.PetTypeId,
                    principalTable: "PetType",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PetSitterServiceType_PetTypeId",
            table: "PetSitterServiceType",
            column: "PetTypeId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PetSitterServiceType");

        migrationBuilder.DropTable(
            name: "PetType");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "PetSitter");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "PetSitter");
    }
}
