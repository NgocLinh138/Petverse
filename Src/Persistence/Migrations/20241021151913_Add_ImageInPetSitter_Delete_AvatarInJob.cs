using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Add_ImageInPetSitter_Delete_AvatarInJob : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Avatar",
            table: "Job");

        migrationBuilder.AddColumn<string>(
            name: "Image",
            table: "PetSitter",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Image",
            table: "PetSitter");

        migrationBuilder.AddColumn<string>(
            name: "Avatar",
            table: "Job",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }
}
