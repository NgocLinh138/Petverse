using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCertificationConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certification_Application_ApplicationId",
                table: "Certification");

            migrationBuilder.AddForeignKey(
                name: "FK_Certification_Application_ApplicationId",
                table: "Certification",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certification_Application_ApplicationId",
                table: "Certification");

            migrationBuilder.AddForeignKey(
                name: "FK_Certification_Application_ApplicationId",
                table: "Certification",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id");
        }
    }
}
