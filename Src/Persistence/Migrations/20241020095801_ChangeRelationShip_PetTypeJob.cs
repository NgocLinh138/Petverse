using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationShip_PetTypeJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetSitterServiceType");

            migrationBuilder.CreateTable(
                name: "PetTypeJob",
                columns: table => new
                {
                    PetTypeId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetTypeJob", x => new { x.JobId, x.PetTypeId });
                    table.ForeignKey(
                        name: "FK_PetTypeJob_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PetTypeJob_PetType_PetTypeId",
                        column: x => x.PetTypeId,
                        principalTable: "PetType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetTypeJob_PetTypeId",
                table: "PetTypeJob",
                column: "PetTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetTypeJob");

            migrationBuilder.CreateTable(
                name: "PetSitterServiceType",
                columns: table => new
                {
                    PetSitterServiceId = table.Column<int>(type: "int", nullable: false),
                    PetTypeId = table.Column<int>(type: "int", nullable: false)
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
    }
}
