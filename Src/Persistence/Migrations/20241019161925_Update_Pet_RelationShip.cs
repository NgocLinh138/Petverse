using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_Pet_RelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breed_PetType_PetTypeId",
                table: "Breed");

            migrationBuilder.DropForeignKey(
                name: "FK_PetSitterServiceType_PetSitterService_PetSitterServiceId",
                table: "PetSitterServiceType");

            migrationBuilder.DropForeignKey(
                name: "FK_PetSitterServiceType_PetType_PetTypeId",
                table: "PetSitterServiceType");

            migrationBuilder.DropForeignKey(
                name: "FK_PetSubType_PetType_PetTypeId",
                table: "PetSubType");

            migrationBuilder.AlterColumn<int>(
                name: "BreedId",
                table: "Pet",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PetSubTypeId",
                table: "Pet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pet_PetSubTypeId",
                table: "Pet",
                column: "PetSubTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Breed_PetType_PetTypeId",
                table: "Breed",
                column: "PetTypeId",
                principalTable: "PetType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_PetSubType_PetSubTypeId",
                table: "Pet",
                column: "PetSubTypeId",
                principalTable: "PetSubType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetSitterServiceType_PetSitterService_PetSitterServiceId",
                table: "PetSitterServiceType",
                column: "PetSitterServiceId",
                principalTable: "PetSitterService",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetSitterServiceType_PetType_PetTypeId",
                table: "PetSitterServiceType",
                column: "PetTypeId",
                principalTable: "PetType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetSubType_PetType_PetTypeId",
                table: "PetSubType",
                column: "PetTypeId",
                principalTable: "PetType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breed_PetType_PetTypeId",
                table: "Breed");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_PetSubType_PetSubTypeId",
                table: "Pet");

            migrationBuilder.DropForeignKey(
                name: "FK_PetSitterServiceType_PetSitterService_PetSitterServiceId",
                table: "PetSitterServiceType");

            migrationBuilder.DropForeignKey(
                name: "FK_PetSitterServiceType_PetType_PetTypeId",
                table: "PetSitterServiceType");

            migrationBuilder.DropForeignKey(
                name: "FK_PetSubType_PetType_PetTypeId",
                table: "PetSubType");

            migrationBuilder.DropIndex(
                name: "IX_Pet_PetSubTypeId",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "PetSubTypeId",
                table: "Pet");

            migrationBuilder.AlterColumn<int>(
                name: "BreedId",
                table: "Pet",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Breed_PetType_PetTypeId",
                table: "Breed",
                column: "PetTypeId",
                principalTable: "PetType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetSitterServiceType_PetSitterService_PetSitterServiceId",
                table: "PetSitterServiceType",
                column: "PetSitterServiceId",
                principalTable: "PetSitterService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetSitterServiceType_PetType_PetTypeId",
                table: "PetSitterServiceType",
                column: "PetTypeId",
                principalTable: "PetType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetSubType_PetType_PetTypeId",
                table: "PetSubType",
                column: "PetTypeId",
                principalTable: "PetType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
