using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "ReportImage",
                newName: "URL");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ReportImage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ReportImage");

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "ReportImage",
                newName: "Image");
        }
    }
}
