using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class UpdateDateTime : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreateDate",
            table: "Transaction");

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "Transaction",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AlterColumn<DateTime>(
            name: "Time",
            table: "Schedule",
            type: "datetime2(0)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Reply",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<bool>(
            name: "IsDeleted",
            table: "Reply",
            type: "bit",
            nullable: false,
            defaultValue: false,
            oldClrType: typeof(bool),
            oldType: "bit");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedDate",
            table: "Reply",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Reply",
            type: "datetime2(0)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Post",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<bool>(
            name: "IsDeleted",
            table: "Post",
            type: "bit",
            nullable: false,
            defaultValue: false,
            oldClrType: typeof(bool),
            oldType: "bit");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedDate",
            table: "Post",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Post",
            type: "datetime2(0)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AlterColumn<DateTime>(
            name: "VaccineDate",
            table: "PetVaccination",
            type: "datetime2(0)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "PetSitterRate",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<bool>(
            name: "IsDeleted",
            table: "PetSitterRate",
            type: "bit",
            nullable: false,
            defaultValue: false,
            oldClrType: typeof(bool),
            oldType: "bit");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedDate",
            table: "PetSitterRate",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "PetSitterRate",
            type: "datetime2(0)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "PetPhoto",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "PetPhoto",
            type: "datetime2(0)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Pet",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<bool>(
            name: "IsDeleted",
            table: "Pet",
            type: "bit",
            nullable: false,
            defaultValue: false,
            oldClrType: typeof(bool),
            oldType: "bit");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedDate",
            table: "Pet",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Pet",
            type: "datetime2(0)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Comment",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<bool>(
            name: "IsDeleted",
            table: "Comment",
            type: "bit",
            nullable: false,
            defaultValue: false,
            oldClrType: typeof(bool),
            oldType: "bit");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedDate",
            table: "Comment",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Comment",
            type: "datetime2(0)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AddColumn<DateTime>(
            name: "Time",
            table: "ChatHistory",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "AppUser",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<bool>(
            name: "IsDeleted",
            table: "AppUser",
            type: "bit",
            nullable: false,
            defaultValue: false,
            oldClrType: typeof(bool),
            oldType: "bit");

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedDate",
            table: "AppUser",
            type: "datetime2(0)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "DateOfBirth",
            table: "AppUser",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "AppUser",
            type: "datetime2(0)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AddColumn<DateTime>(
            name: "DateOfBirth",
            table: "Application",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "Transaction");

        migrationBuilder.DropColumn(
            name: "Time",
            table: "ChatHistory");

        migrationBuilder.DropColumn(
            name: "DateOfBirth",
            table: "Application");

        migrationBuilder.AddColumn<DateTime>(
            name: "CreateDate",
            table: "Transaction",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AlterColumn<DateTime>(
            name: "Time",
            table: "Schedule",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Reply",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);

        migrationBuilder.AlterColumn<bool>(
            name: "IsDeleted",
            table: "Reply",
            type: "bit",
            nullable: false,
            oldClrType: typeof(bool),
            oldType: "bit",
            oldDefaultValue: false);

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedDate",
            table: "Reply",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Reply",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Post",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);

        migrationBuilder.AlterColumn<bool>(
            name: "IsDeleted",
            table: "Post",
            type: "bit",
            nullable: false,
            oldClrType: typeof(bool),
            oldType: "bit",
            oldDefaultValue: false);

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedDate",
            table: "Post",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Post",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "VaccineDate",
            table: "PetVaccination",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "PetSitterRate",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);

        migrationBuilder.AlterColumn<bool>(
            name: "IsDeleted",
            table: "PetSitterRate",
            type: "bit",
            nullable: false,
            oldClrType: typeof(bool),
            oldType: "bit",
            oldDefaultValue: false);

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedDate",
            table: "PetSitterRate",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "PetSitterRate",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "PetPhoto",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "PetPhoto",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Pet",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);

        migrationBuilder.AlterColumn<bool>(
            name: "IsDeleted",
            table: "Pet",
            type: "bit",
            nullable: false,
            oldClrType: typeof(bool),
            oldType: "bit",
            oldDefaultValue: false);

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedDate",
            table: "Pet",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Pet",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "Comment",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);

        migrationBuilder.AlterColumn<bool>(
            name: "IsDeleted",
            table: "Comment",
            type: "bit",
            nullable: false,
            oldClrType: typeof(bool),
            oldType: "bit",
            oldDefaultValue: false);

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedDate",
            table: "Comment",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Comment",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedDate",
            table: "AppUser",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);

        migrationBuilder.AlterColumn<bool>(
            name: "IsDeleted",
            table: "AppUser",
            type: "bit",
            nullable: false,
            oldClrType: typeof(bool),
            oldType: "bit",
            oldDefaultValue: false);

        migrationBuilder.AlterColumn<DateTime>(
            name: "DeletedDate",
            table: "AppUser",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "DateOfBirth",
            table: "AppUser",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "AppUser",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)");
    }
}
