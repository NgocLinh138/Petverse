using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSpeciesPlace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthAge",
                table: "Vaccine");

            migrationBuilder.AddColumn<int>(
                name: "MinAge",
                table: "Vaccine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SpeciesPlace",
                columns: table => new
                {
                    PlaceId = table.Column<int>(type: "int", nullable: false),
                    SpeciesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeciesPlace", x => new { x.PlaceId, x.SpeciesId });
                    table.ForeignKey(
                        name: "FK_SpeciesPlace_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpeciesPlace_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpeciesPlace_SpeciesId",
                table: "SpeciesPlace",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeciesPlace_PlaceId",
                table: "SpeciesPlace",
                column: "PlaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpeciesPlace");

            migrationBuilder.DropColumn(
                name: "MinAge",
                table: "Vaccine");

            migrationBuilder.AddColumn<string>(
                name: "MonthAge",
                table: "Vaccine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
