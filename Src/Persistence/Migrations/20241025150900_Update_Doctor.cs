using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Update_Doctor : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Description",
            table: "Doctor");

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "Doctor",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "Doctor",
            type: "datetime2(0)",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Doctor",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsVerified",
            table: "Doctor",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "Doctor",
            type: "datetime2(0)",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "DoctorRate",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Rate = table.Column<float>(type: "real", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                DeletedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DoctorRate", x => x.Id);
                table.ForeignKey(
                    name: "FK_DoctorRate_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_DoctorRate_Doctor_DoctorId",
                    column: x => x.DoctorId,
                    principalTable: "Doctor",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_DoctorRate_DoctorId",
            table: "DoctorRate",
            column: "DoctorId");

        migrationBuilder.CreateIndex(
            name: "IX_DoctorRate_UserId",
            table: "DoctorRate",
            column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "DoctorRate");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "Doctor");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "Doctor");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Doctor");

        migrationBuilder.DropColumn(
            name: "IsVerified",
            table: "Doctor");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "Doctor");

        migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "Doctor",
            type: "nvarchar(max)",
            nullable: true);
    }
}
