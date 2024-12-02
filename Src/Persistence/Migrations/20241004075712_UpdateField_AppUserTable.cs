using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class UpdateField_AppUserTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "IsActive",
            table: "AppUser",
            newName: "IsDeleted");

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Reply",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Post",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "PetSitterRate",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Pet",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Comment",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Reply");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Post");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "PetSitterRate");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Pet");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Comment");

        migrationBuilder.RenameColumn(
            name: "IsDeleted",
            table: "AppUser",
            newName: "IsActive");
    }
}
