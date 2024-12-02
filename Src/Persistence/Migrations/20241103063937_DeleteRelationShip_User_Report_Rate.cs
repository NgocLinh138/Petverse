using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteRelationShip_User_Report_Rate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetCenterRate_AppUser_UserId",
                table: "PetCenterRate");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_AppUser_UserId",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_UserId",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_PetCenterRate_UserId",
                table: "PetCenterRate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Report_UserId",
                table: "Report",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PetCenterRate_UserId",
                table: "PetCenterRate",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetCenterRate_AppUser_UserId",
                table: "PetCenterRate",
                column: "UserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_AppUser_UserId",
                table: "Report",
                column: "UserId",
                principalTable: "AppUser",
                principalColumn: "Id");
        }
    }
}
