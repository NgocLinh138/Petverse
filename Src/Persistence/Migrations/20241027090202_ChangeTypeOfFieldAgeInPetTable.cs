using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeOfFieldAgeInPetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
             name: "BirthDate",
             table: "Pet",
             type: "datetime2",
             nullable: false,
             defaultValue: DateTime.Now);

            // Step 2: Populate BirthDate from Age
            migrationBuilder.Sql(@"
            UPDATE Pet
            SET BirthDate = DATEADD(YEAR, -Age, GETDATE())
        ");

            // Step 3: Drop the old Age column
            migrationBuilder.DropColumn(name: "Age", table: "Pet");

            // Step 4: Rename BirthDate to Age (if necessary)
            migrationBuilder.RenameColumn("BirthDate", "Pet", "Age");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
           name: "Age",
           table: "Pet",
           type: "int",
           nullable: false,
           defaultValue: 0);

            // Step 2: Populate Age from BirthDate
            migrationBuilder.Sql(@"
            UPDATE Pet
            SET Age = DATEDIFF(YEAR, BirthDate, GETDATE())
        ");

            // Step 3: Drop the BirthDate column
            migrationBuilder.DropColumn(name: "BirthDate", table: "Pet");
        }
    }
}
