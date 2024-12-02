using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Add_CenterBreedImage : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Date",
            table: "Schedule");

        migrationBuilder.DropColumn(
            name: "Image",
            table: "CenterBreed");

        migrationBuilder.CreateTable(
            name: "CenterBreedImage",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CenterBreedId = table.Column<int>(type: "int", nullable: false),
                Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CenterBreedImage", x => x.Id);
                table.ForeignKey(
                    name: "FK_CenterBreedImage_CenterBreed_CenterBreedId",
                    column: x => x.CenterBreedId,
                    principalTable: "CenterBreed",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_CenterBreedImage_CenterBreedId",
            table: "CenterBreedImage",
            column: "CenterBreedId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CenterBreedImage");

        migrationBuilder.AddColumn<DateTime>(
            name: "Date",
            table: "Schedule",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<string>(
            name: "Image",
            table: "CenterBreed",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }
}
