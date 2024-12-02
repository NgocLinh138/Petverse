using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class DeleteChannel : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "UserChannel");

        migrationBuilder.DropTable(
            name: "Channel");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Channel",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Channel", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "UserChannel",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ChannelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserChannel", x => new { x.UserId, x.ChannelId });
                table.ForeignKey(
                    name: "FK_UserChannel_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_UserChannel_Channel_ChannelId",
                    column: x => x.ChannelId,
                    principalTable: "Channel",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_UserChannel_ChannelId",
            table: "UserChannel",
            column: "ChannelId");
    }
}
