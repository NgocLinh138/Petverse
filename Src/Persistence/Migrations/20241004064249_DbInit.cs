using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class DbInit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AppRole",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppRole", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Breed",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetType = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Breed", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ChatRoom",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ChatRoom", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PetService",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetService", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AppUser",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Gender = table.Column<int>(type: "int", nullable: true),
                DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppUser", x => x.Id);
                table.ForeignKey(
                    name: "FK_AppUser_AppRole_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AppRole",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Application",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Certification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CancelReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Application", x => x.Id);
                table.ForeignKey(
                    name: "FK_Application_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ChatHistory",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ChatHistory", x => x.Id);
                table.ForeignKey(
                    name: "FK_ChatHistory_AppUser_ChatRoomId",
                    column: x => x.ChatRoomId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ChatHistory_ChatRoom_ChatRoomId",
                    column: x => x.ChatRoomId,
                    principalTable: "ChatRoom",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Pet",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                BreedId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                Age = table.Column<int>(type: "int", nullable: false),
                Gender = table.Column<int>(type: "int", nullable: false),
                Weight = table.Column<float>(type: "real", nullable: false),
                Sterilized = table.Column<bool>(type: "bit", nullable: false),
                Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Pet", x => x.Id);
                table.ForeignKey(
                    name: "FK_Pet_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Pet_Breed_BreedId",
                    column: x => x.BreedId,
                    principalTable: "Breed",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "PetSitter",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Rating = table.Column<float>(type: "real", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetSitter", x => x.Id);
                table.ForeignKey(
                    name: "FK_PetSitter_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Post",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetId = table.Column<int>(type: "int", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Post", x => x.Id);
                table.ForeignKey(
                    name: "FK_Post_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Transaction",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Transaction", x => x.Id);
                table.ForeignKey(
                    name: "FK_Transaction_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "UserChatRoom",
            columns: table => new
            {
                ChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserChatRoom", x => new { x.ChatRoomId, x.UserId });
                table.ForeignKey(
                    name: "FK_UserChatRoom_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_UserChatRoom_ChatRoom_ChatRoomId",
                    column: x => x.ChatRoomId,
                    principalTable: "ChatRoom",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "ApplicationPetService",
            columns: table => new
            {
                ApplicationId = table.Column<int>(type: "int", nullable: false),
                PetServiceId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationPetService", x => new { x.ApplicationId, x.PetServiceId });
                table.ForeignKey(
                    name: "FK_ApplicationPetService_Application_ApplicationId",
                    column: x => x.ApplicationId,
                    principalTable: "Application",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ApplicationPetService_PetService_PetServiceId",
                    column: x => x.PetServiceId,
                    principalTable: "PetService",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PetPhoto",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetId = table.Column<int>(type: "int", nullable: false),
                Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetPhoto", x => x.Id);
                table.ForeignKey(
                    name: "FK_PetPhoto_Pet_PetId",
                    column: x => x.PetId,
                    principalTable: "Pet",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "PetVaccination",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Certification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                VaccineDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetVaccination", x => x.Id);
                table.ForeignKey(
                    name: "FK_PetVaccination_Pet_PetId",
                    column: x => x.PetId,
                    principalTable: "Pet",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Schedule",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetId = table.Column<int>(type: "int", nullable: false),
                Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Schedule", x => x.Id);
                table.ForeignKey(
                    name: "FK_Schedule_Pet_PetId",
                    column: x => x.PetId,
                    principalTable: "Pet",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "PetSitterService",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetSitterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetServiceId = table.Column<int>(type: "int", nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Rating = table.Column<float>(type: "real", nullable: true),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetSitterService", x => x.Id);
                table.ForeignKey(
                    name: "FK_PetSitterService_PetService_PetServiceId",
                    column: x => x.PetServiceId,
                    principalTable: "PetService",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_PetSitterService_PetSitter_PetSitterId",
                    column: x => x.PetSitterId,
                    principalTable: "PetSitter",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Report",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetSitterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Report", x => x.Id);
                table.ForeignKey(
                    name: "FK_Report_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Report_PetSitter_PetSitterId",
                    column: x => x.PetSitterId,
                    principalTable: "PetSitter",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "WorkHistory",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetSitterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkHistory", x => x.Id);
                table.ForeignKey(
                    name: "FK_WorkHistory_PetSitter_PetSitterId",
                    column: x => x.PetSitterId,
                    principalTable: "PetSitter",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Comment",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PostId = table.Column<int>(type: "int", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Comment", x => x.Id);
                table.ForeignKey(
                    name: "FK_Comment_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Comment_Post_PostId",
                    column: x => x.PostId,
                    principalTable: "Post",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Photo",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PostId = table.Column<int>(type: "int", nullable: false),
                Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ONum = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Photo", x => x.Id);
                table.ForeignKey(
                    name: "FK_Photo_Post_PostId",
                    column: x => x.PostId,
                    principalTable: "Post",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PostLike",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PostId = table.Column<int>(type: "int", nullable: false),
                IsLike = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PostLike", x => x.Id);
                table.ForeignKey(
                    name: "FK_PostLike_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_PostLike_Post_PostId",
                    column: x => x.PostId,
                    principalTable: "Post",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "PostPet",
            columns: table => new
            {
                PostId = table.Column<int>(type: "int", nullable: false),
                PetId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PostPet", x => new { x.PostId, x.PetId });
                table.ForeignKey(
                    name: "FK_PostPet_Pet_PetId",
                    column: x => x.PetId,
                    principalTable: "Pet",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_PostPet_Post_PostId",
                    column: x => x.PostId,
                    principalTable: "Post",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "PetSitterRate",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetSitterServiceId = table.Column<int>(type: "int", nullable: false),
                Rating = table.Column<float>(type: "real", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetSitterRate", x => x.Id);
                table.ForeignKey(
                    name: "FK_PetSitterRate_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_PetSitterRate_PetSitterService_PetSitterServiceId",
                    column: x => x.PetSitterServiceId,
                    principalTable: "PetSitterService",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "CommentLike",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CommentId = table.Column<int>(type: "int", nullable: false),
                IsLike = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CommentLike", x => x.Id);
                table.ForeignKey(
                    name: "FK_CommentLike_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_CommentLike_Comment_CommentId",
                    column: x => x.CommentId,
                    principalTable: "Comment",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Reply",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CommentId = table.Column<int>(type: "int", nullable: false),
                Content = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reply", x => x.Id);
                table.ForeignKey(
                    name: "FK_Reply_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Reply_Comment_CommentId",
                    column: x => x.CommentId,
                    principalTable: "Comment",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "ReplyLike",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ReplyId = table.Column<int>(type: "int", nullable: false),
                IsLike = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReplyLike", x => x.Id);
                table.ForeignKey(
                    name: "FK_ReplyLike_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ReplyLike_Reply_ReplyId",
                    column: x => x.ReplyId,
                    principalTable: "Reply",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Application_UserId",
            table: "Application",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_ApplicationPetService_PetServiceId",
            table: "ApplicationPetService",
            column: "PetServiceId");

        migrationBuilder.CreateIndex(
            name: "IX_AppUser_RoleId",
            table: "AppUser",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_ChatHistory_ChatRoomId",
            table: "ChatHistory",
            column: "ChatRoomId");

        migrationBuilder.CreateIndex(
            name: "IX_Comment_PostId",
            table: "Comment",
            column: "PostId");

        migrationBuilder.CreateIndex(
            name: "IX_Comment_UserId",
            table: "Comment",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_CommentLike_CommentId",
            table: "CommentLike",
            column: "CommentId");

        migrationBuilder.CreateIndex(
            name: "IX_CommentLike_UserId",
            table: "CommentLike",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Pet_BreedId",
            table: "Pet",
            column: "BreedId");

        migrationBuilder.CreateIndex(
            name: "IX_Pet_UserId",
            table: "Pet",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_PetPhoto_PetId",
            table: "PetPhoto",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_PetSitter_UserId",
            table: "PetSitter",
            column: "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_PetSitterRate_PetSitterServiceId",
            table: "PetSitterRate",
            column: "PetSitterServiceId");

        migrationBuilder.CreateIndex(
            name: "IX_PetSitterRate_UserId",
            table: "PetSitterRate",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_PetSitterService_PetServiceId",
            table: "PetSitterService",
            column: "PetServiceId");

        migrationBuilder.CreateIndex(
            name: "IX_PetSitterService_PetSitterId",
            table: "PetSitterService",
            column: "PetSitterId");

        migrationBuilder.CreateIndex(
            name: "IX_PetVaccination_PetId",
            table: "PetVaccination",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_Photo_PostId",
            table: "Photo",
            column: "PostId");

        migrationBuilder.CreateIndex(
            name: "IX_Post_UserId",
            table: "Post",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_PostLike_PostId",
            table: "PostLike",
            column: "PostId");

        migrationBuilder.CreateIndex(
            name: "IX_PostLike_UserId",
            table: "PostLike",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_PostPet_PetId",
            table: "PostPet",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_Reply_CommentId",
            table: "Reply",
            column: "CommentId");

        migrationBuilder.CreateIndex(
            name: "IX_Reply_UserId",
            table: "Reply",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_ReplyLike_ReplyId",
            table: "ReplyLike",
            column: "ReplyId");

        migrationBuilder.CreateIndex(
            name: "IX_ReplyLike_UserId",
            table: "ReplyLike",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Report_PetSitterId",
            table: "Report",
            column: "PetSitterId");

        migrationBuilder.CreateIndex(
            name: "IX_Report_UserId",
            table: "Report",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Schedule_PetId",
            table: "Schedule",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_Transaction_UserId",
            table: "Transaction",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserChatRoom_UserId",
            table: "UserChatRoom",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_WorkHistory_PetSitterId",
            table: "WorkHistory",
            column: "PetSitterId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ApplicationPetService");

        migrationBuilder.DropTable(
            name: "ChatHistory");

        migrationBuilder.DropTable(
            name: "CommentLike");

        migrationBuilder.DropTable(
            name: "PetPhoto");

        migrationBuilder.DropTable(
            name: "PetSitterRate");

        migrationBuilder.DropTable(
            name: "PetVaccination");

        migrationBuilder.DropTable(
            name: "Photo");

        migrationBuilder.DropTable(
            name: "PostLike");

        migrationBuilder.DropTable(
            name: "PostPet");

        migrationBuilder.DropTable(
            name: "ReplyLike");

        migrationBuilder.DropTable(
            name: "Report");

        migrationBuilder.DropTable(
            name: "Schedule");

        migrationBuilder.DropTable(
            name: "Transaction");

        migrationBuilder.DropTable(
            name: "UserChatRoom");

        migrationBuilder.DropTable(
            name: "WorkHistory");

        migrationBuilder.DropTable(
            name: "Application");

        migrationBuilder.DropTable(
            name: "PetSitterService");

        migrationBuilder.DropTable(
            name: "Reply");

        migrationBuilder.DropTable(
            name: "Pet");

        migrationBuilder.DropTable(
            name: "ChatRoom");

        migrationBuilder.DropTable(
            name: "PetService");

        migrationBuilder.DropTable(
            name: "PetSitter");

        migrationBuilder.DropTable(
            name: "Comment");

        migrationBuilder.DropTable(
            name: "Breed");

        migrationBuilder.DropTable(
            name: "Post");

        migrationBuilder.DropTable(
            name: "AppUser");

        migrationBuilder.DropTable(
            name: "AppRole");
    }
}
