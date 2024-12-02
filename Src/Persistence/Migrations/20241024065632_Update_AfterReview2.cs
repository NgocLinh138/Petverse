using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Update_AfterReview2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Appointment_AppUser_UserId",
            table: "Appointment");

        migrationBuilder.DropForeignKey(
            name: "FK_Appointment_PetSitterService_PetSitterServiceId",
            table: "Appointment");

        migrationBuilder.DropForeignKey(
            name: "FK_Appointment_Pet_PetId",
            table: "Appointment");

        migrationBuilder.DropForeignKey(
            name: "FK_Breed_PetType_PetTypeId",
            table: "Breed");

        migrationBuilder.DropForeignKey(
            name: "FK_Job_PetSitter_PetSitterId",
            table: "Job");

        migrationBuilder.DropForeignKey(
            name: "FK_Pet_PetSubType_PetSubTypeId",
            table: "Pet");

        migrationBuilder.DropForeignKey(
            name: "FK_Report_AppUser_UserId",
            table: "Report");

        migrationBuilder.DropForeignKey(
            name: "FK_Report_PetSitter_PetSitterId",
            table: "Report");

        migrationBuilder.DropForeignKey(
            name: "FK_Schedule_Pet_PetId",
            table: "Schedule");

        migrationBuilder.DropTable(
            name: "PetPhoto");

        migrationBuilder.DropTable(
            name: "PetSitterRate");

        migrationBuilder.DropTable(
            name: "PetSubType");

        migrationBuilder.DropTable(
            name: "PetTypeJob");

        migrationBuilder.DropTable(
            name: "PetVaccination");

        migrationBuilder.DropTable(
            name: "TimeTable");

        migrationBuilder.DropTable(
            name: "Transaction");

        migrationBuilder.DropTable(
            name: "WorkHistory");

        migrationBuilder.DropTable(
            name: "PetSitterService");

        migrationBuilder.DropTable(
            name: "PetType");

        migrationBuilder.DropTable(
            name: "PetSitter");

        migrationBuilder.DropIndex(
            name: "IX_Schedule_PetId",
            table: "Schedule");

        migrationBuilder.DropIndex(
            name: "IX_Report_PetSitterId",
            table: "Report");

        migrationBuilder.DropIndex(
            name: "IX_Pet_PetSubTypeId",
            table: "Pet");

        migrationBuilder.DropIndex(
            name: "IX_Appointment_PetId",
            table: "Appointment");

        migrationBuilder.DropIndex(
            name: "IX_Appointment_PetSitterServiceId",
            table: "Appointment");

        migrationBuilder.DropColumn(
            name: "PetId",
            table: "Schedule");

        migrationBuilder.DropColumn(
            name: "Type",
            table: "Schedule");

        migrationBuilder.DropColumn(
            name: "PetSubTypeId",
            table: "Pet");

        migrationBuilder.DropColumn(
            name: "Price",
            table: "Job");

        migrationBuilder.DropColumn(
            name: "Address",
            table: "Appointment");

        migrationBuilder.DropColumn(
            name: "Note",
            table: "Appointment");

        migrationBuilder.DropColumn(
            name: "PetId",
            table: "Appointment");

        migrationBuilder.RenameColumn(
            name: "PetSitterId",
            table: "Report",
            newName: "PetCenterId");

        migrationBuilder.RenameColumn(
            name: "PetSitterId",
            table: "Job",
            newName: "PetCenterId");

        migrationBuilder.RenameIndex(
            name: "IX_Job_PetSitterId",
            table: "Job",
            newName: "IX_Job_PetCenterId");

        migrationBuilder.RenameColumn(
            name: "PetTypeId",
            table: "Breed",
            newName: "SpeciesId");

        migrationBuilder.RenameIndex(
            name: "IX_Breed_PetTypeId",
            table: "Breed",
            newName: "IX_Breed_SpeciesId");

        migrationBuilder.RenameColumn(
            name: "PetSitterServiceId",
            table: "Appointment",
            newName: "Type");

        migrationBuilder.AlterColumn<string>(
            name: "Time",
            table: "Schedule",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(0)");

        migrationBuilder.AddColumn<DateTime>(
            name: "Date",
            table: "Schedule",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "Schedule",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<Guid>(
            name: "ServiceAppointmentId",
            table: "Schedule",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AlterColumn<int>(
            name: "BreedId",
            table: "Pet",
            type: "int",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Breed",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "UserName",
            table: "AppUser",
            type: "nvarchar(256)",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "NormalizedUserName",
            table: "AppUser",
            type: "nvarchar(256)",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "NormalizedEmail",
            table: "AppUser",
            type: "nvarchar(256)",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "AppUser",
            type: "nvarchar(256)",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "NormalizedName",
            table: "AppRole",
            type: "nvarchar(256)",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "AppRole",
            type: "nvarchar(256)",
            maxLength: 256,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "Appointment",
            type: "datetime2(0)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AlterColumn<string>(
            name: "PhoneNumber",
            table: "Application",
            type: "nvarchar(15)",
            maxLength: 15,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(10)",
            oldMaxLength: 10);

        migrationBuilder.AddColumn<int>(
            name: "Type",
            table: "Application",
            type: "int",
            nullable: false,
            defaultValue: 0);

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
            name: "Doctor",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                StartTime = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                EndTime = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Doctor", x => x.Id);
                table.ForeignKey(
                    name: "FK_Doctor_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Payment",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                PaymentDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Payment", x => x.Id);
                table.ForeignKey(
                    name: "FK_Payment_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Payment_Appointment_AppointmentId",
                    column: x => x.AppointmentId,
                    principalTable: "Appointment",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PetCenter",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                HaveTransport = table.Column<bool>(type: "bit", nullable: false),
                NumPet = table.Column<int>(type: "int", nullable: false),
                IsVerified = table.Column<bool>(type: "bit", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetCenter", x => x.Id);
                table.ForeignKey(
                    name: "FK_PetCenter_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "PetVaccinated",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DateVaccinated = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetVaccinated", x => x.Id);
                table.ForeignKey(
                    name: "FK_PetVaccinated_Pet_PetId",
                    column: x => x.PetId,
                    principalTable: "Pet",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Photo",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetId = table.Column<int>(type: "int", nullable: false),
                Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Photo", x => x.Id);
                table.ForeignKey(
                    name: "FK_Photo_Pet_PetId",
                    column: x => x.PetId,
                    principalTable: "Pet",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Place",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Type = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Lng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Place", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Species",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Species", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Tracking",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ScheduleId = table.Column<int>(type: "int", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UploadedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tracking", x => x.Id);
                table.ForeignKey(
                    name: "FK_Tracking_Schedule_ScheduleId",
                    column: x => x.ScheduleId,
                    principalTable: "Schedule",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Warning",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetId = table.Column<int>(type: "int", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Warning", x => x.Id);
                table.ForeignKey(
                    name: "FK_Warning_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Warning_Pet_PetId",
                    column: x => x.PetId,
                    principalTable: "Pet",
                    principalColumn: "Id");
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
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "DoctorAppointment",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DoctorAppointment", x => x.Id);
                table.ForeignKey(
                    name: "FK_DoctorAppointment_AppUser_AppUserId",
                    column: x => x.AppUserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_DoctorAppointment_Appointment_Id",
                    column: x => x.Id,
                    principalTable: "Appointment",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_DoctorAppointment_Doctor_DoctorId",
                    column: x => x.DoctorId,
                    principalTable: "Doctor",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PetCenterService",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetServiceId = table.Column<int>(type: "int", nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Rate = table.Column<float>(type: "real", nullable: true),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Type = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetCenterService", x => x.Id);
                table.ForeignKey(
                    name: "FK_PetCenterService_PetCenter_PetCenterId",
                    column: x => x.PetCenterId,
                    principalTable: "PetCenter",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_PetCenterService_PetService_PetServiceId",
                    column: x => x.PetServiceId,
                    principalTable: "PetService",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "CenterBreed",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SpeciesId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CenterBreed", x => x.Id);
                table.ForeignKey(
                    name: "FK_CenterBreed_PetCenter_PetCenterId",
                    column: x => x.PetCenterId,
                    principalTable: "PetCenter",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CenterBreed_Species_SpeciesId",
                    column: x => x.SpeciesId,
                    principalTable: "Species",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "JobSpecies",
            columns: table => new
            {
                SpeciesId = table.Column<int>(type: "int", nullable: false),
                JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JobSpecies", x => new { x.JobId, x.SpeciesId });
                table.ForeignKey(
                    name: "FK_JobSpecies_Job_JobId",
                    column: x => x.JobId,
                    principalTable: "Job",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_JobSpecies_Species_SpeciesId",
                    column: x => x.SpeciesId,
                    principalTable: "Species",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Vaccine",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                SpeciesId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                MonthAge = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Vaccine", x => x.Id);
                table.ForeignKey(
                    name: "FK_Vaccine_Species_SpeciesId",
                    column: x => x.SpeciesId,
                    principalTable: "Species",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "PetCenterRate",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetCenterServiceId = table.Column<int>(type: "int", nullable: false),
                Rate = table.Column<float>(type: "real", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                DeletedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetCenterRate", x => x.Id);
                table.ForeignKey(
                    name: "FK_PetCenterRate_AppUser_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_PetCenterRate_PetCenterService_PetCenterServiceId",
                    column: x => x.PetCenterServiceId,
                    principalTable: "PetCenterService",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ServiceAppointment",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetId = table.Column<int>(type: "int", nullable: false),
                PetCenterServiceId = table.Column<int>(type: "int", nullable: false),
                AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ServiceAppointment", x => x.Id);
                table.ForeignKey(
                    name: "FK_ServiceAppointment_AppUser_AppUserId",
                    column: x => x.AppUserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ServiceAppointment_Appointment_Id",
                    column: x => x.Id,
                    principalTable: "Appointment",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ServiceAppointment_PetCenterService_PetCenterServiceId",
                    column: x => x.PetCenterServiceId,
                    principalTable: "PetCenterService",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_ServiceAppointment_Pet_PetId",
                    column: x => x.PetId,
                    principalTable: "Pet",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "BreedAppointment",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetId = table.Column<int>(type: "int", nullable: false),
                CenterBreedId = table.Column<int>(type: "int", nullable: false),
                AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BreedAppointment", x => x.Id);
                table.ForeignKey(
                    name: "FK_BreedAppointment_AppUser_AppUserId",
                    column: x => x.AppUserId,
                    principalTable: "AppUser",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_BreedAppointment_Appointment_Id",
                    column: x => x.Id,
                    principalTable: "Appointment",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_BreedAppointment_CenterBreed_CenterBreedId",
                    column: x => x.CenterBreedId,
                    principalTable: "CenterBreed",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_BreedAppointment_Pet_PetId",
                    column: x => x.PetId,
                    principalTable: "Pet",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "VaccineRecommendation",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                VaccineId = table.Column<int>(type: "int", nullable: false),
                PetId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_VaccineRecommendation", x => x.Id);
                table.ForeignKey(
                    name: "FK_VaccineRecommendation_Pet_PetId",
                    column: x => x.PetId,
                    principalTable: "Pet",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_VaccineRecommendation_Vaccine_VaccineId",
                    column: x => x.VaccineId,
                    principalTable: "Vaccine",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Schedule_ServiceAppointmentId",
            table: "Schedule",
            column: "ServiceAppointmentId");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            table: "AppUser",
            column: "NormalizedEmail");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            table: "AppUser",
            column: "NormalizedUserName",
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "AppRole",
            column: "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_BreedAppointment_AppUserId",
            table: "BreedAppointment",
            column: "AppUserId");

        migrationBuilder.CreateIndex(
            name: "IX_BreedAppointment_CenterBreedId",
            table: "BreedAppointment",
            column: "CenterBreedId");

        migrationBuilder.CreateIndex(
            name: "IX_BreedAppointment_PetId",
            table: "BreedAppointment",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_CenterBreed_PetCenterId",
            table: "CenterBreed",
            column: "PetCenterId");

        migrationBuilder.CreateIndex(
            name: "IX_CenterBreed_SpeciesId",
            table: "CenterBreed",
            column: "SpeciesId");

        migrationBuilder.CreateIndex(
            name: "IX_Doctor_UserId",
            table: "Doctor",
            column: "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_DoctorAppointment_AppUserId",
            table: "DoctorAppointment",
            column: "AppUserId");

        migrationBuilder.CreateIndex(
            name: "IX_DoctorAppointment_DoctorId",
            table: "DoctorAppointment",
            column: "DoctorId");

        migrationBuilder.CreateIndex(
            name: "IX_JobSpecies_SpeciesId",
            table: "JobSpecies",
            column: "SpeciesId");

        migrationBuilder.CreateIndex(
            name: "IX_Payment_AppointmentId",
            table: "Payment",
            column: "AppointmentId");

        migrationBuilder.CreateIndex(
            name: "IX_Payment_UserId",
            table: "Payment",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_PetCenter_UserId",
            table: "PetCenter",
            column: "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_PetCenterRate_PetCenterServiceId",
            table: "PetCenterRate",
            column: "PetCenterServiceId");

        migrationBuilder.CreateIndex(
            name: "IX_PetCenterRate_UserId",
            table: "PetCenterRate",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_PetCenterService_PetCenterId",
            table: "PetCenterService",
            column: "PetCenterId");

        migrationBuilder.CreateIndex(
            name: "IX_PetCenterService_PetServiceId",
            table: "PetCenterService",
            column: "PetServiceId");

        migrationBuilder.CreateIndex(
            name: "IX_PetVaccinated_PetId",
            table: "PetVaccinated",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_Photo_PetId",
            table: "Photo",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_ServiceAppointment_AppUserId",
            table: "ServiceAppointment",
            column: "AppUserId");

        migrationBuilder.CreateIndex(
            name: "IX_ServiceAppointment_PetCenterServiceId",
            table: "ServiceAppointment",
            column: "PetCenterServiceId");

        migrationBuilder.CreateIndex(
            name: "IX_ServiceAppointment_PetId",
            table: "ServiceAppointment",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_Species_Name",
            table: "Species",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Tracking_ScheduleId",
            table: "Tracking",
            column: "ScheduleId");

        migrationBuilder.CreateIndex(
            name: "IX_UserChannel_ChannelId",
            table: "UserChannel",
            column: "ChannelId");

        migrationBuilder.CreateIndex(
            name: "IX_Vaccine_SpeciesId",
            table: "Vaccine",
            column: "SpeciesId");

        migrationBuilder.CreateIndex(
            name: "IX_VaccineRecommendation_PetId",
            table: "VaccineRecommendation",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_VaccineRecommendation_VaccineId",
            table: "VaccineRecommendation",
            column: "VaccineId");

        migrationBuilder.CreateIndex(
            name: "IX_Warning_PetId",
            table: "Warning",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_Warning_UserId",
            table: "Warning",
            column: "UserId");

        migrationBuilder.AddForeignKey(
            name: "FK_Appointment_AppUser_UserId",
            table: "Appointment",
            column: "UserId",
            principalTable: "AppUser",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Breed_Species_SpeciesId",
            table: "Breed",
            column: "SpeciesId",
            principalTable: "Species",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Job_PetCenter_PetCenterId",
            table: "Job",
            column: "PetCenterId",
            principalTable: "PetCenter",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Report_AppUser_UserId",
            table: "Report",
            column: "UserId",
            principalTable: "AppUser",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Report_PetCenter_UserId",
            table: "Report",
            column: "UserId",
            principalTable: "PetCenter",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Schedule_ServiceAppointment_ServiceAppointmentId",
            table: "Schedule",
            column: "ServiceAppointmentId",
            principalTable: "ServiceAppointment",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Appointment_AppUser_UserId",
            table: "Appointment");

        migrationBuilder.DropForeignKey(
            name: "FK_Breed_Species_SpeciesId",
            table: "Breed");

        migrationBuilder.DropForeignKey(
            name: "FK_Job_PetCenter_PetCenterId",
            table: "Job");

        migrationBuilder.DropForeignKey(
            name: "FK_Report_AppUser_UserId",
            table: "Report");

        migrationBuilder.DropForeignKey(
            name: "FK_Report_PetCenter_UserId",
            table: "Report");

        migrationBuilder.DropForeignKey(
            name: "FK_Schedule_ServiceAppointment_ServiceAppointmentId",
            table: "Schedule");

        migrationBuilder.DropTable(
            name: "BreedAppointment");

        migrationBuilder.DropTable(
            name: "DoctorAppointment");

        migrationBuilder.DropTable(
            name: "JobSpecies");

        migrationBuilder.DropTable(
            name: "Payment");

        migrationBuilder.DropTable(
            name: "PetCenterRate");

        migrationBuilder.DropTable(
            name: "PetVaccinated");

        migrationBuilder.DropTable(
            name: "Photo");

        migrationBuilder.DropTable(
            name: "Place");

        migrationBuilder.DropTable(
            name: "ServiceAppointment");

        migrationBuilder.DropTable(
            name: "Tracking");

        migrationBuilder.DropTable(
            name: "UserChannel");

        migrationBuilder.DropTable(
            name: "VaccineRecommendation");

        migrationBuilder.DropTable(
            name: "Warning");

        migrationBuilder.DropTable(
            name: "CenterBreed");

        migrationBuilder.DropTable(
            name: "Doctor");

        migrationBuilder.DropTable(
            name: "PetCenterService");

        migrationBuilder.DropTable(
            name: "Channel");

        migrationBuilder.DropTable(
            name: "Vaccine");

        migrationBuilder.DropTable(
            name: "PetCenter");

        migrationBuilder.DropTable(
            name: "Species");

        migrationBuilder.DropIndex(
            name: "IX_Schedule_ServiceAppointmentId",
            table: "Schedule");

        migrationBuilder.DropIndex(
            name: "EmailIndex",
            table: "AppUser");

        migrationBuilder.DropIndex(
            name: "UserNameIndex",
            table: "AppUser");

        migrationBuilder.DropIndex(
            name: "RoleNameIndex",
            table: "AppRole");

        migrationBuilder.DropColumn(
            name: "Date",
            table: "Schedule");

        migrationBuilder.DropColumn(
            name: "Description",
            table: "Schedule");

        migrationBuilder.DropColumn(
            name: "ServiceAppointmentId",
            table: "Schedule");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "Appointment");

        migrationBuilder.DropColumn(
            name: "Type",
            table: "Application");

        migrationBuilder.RenameColumn(
            name: "PetCenterId",
            table: "Report",
            newName: "PetSitterId");

        migrationBuilder.RenameColumn(
            name: "PetCenterId",
            table: "Job",
            newName: "PetSitterId");

        migrationBuilder.RenameIndex(
            name: "IX_Job_PetCenterId",
            table: "Job",
            newName: "IX_Job_PetSitterId");

        migrationBuilder.RenameColumn(
            name: "SpeciesId",
            table: "Breed",
            newName: "PetTypeId");

        migrationBuilder.RenameIndex(
            name: "IX_Breed_SpeciesId",
            table: "Breed",
            newName: "IX_Breed_PetTypeId");

        migrationBuilder.RenameColumn(
            name: "Type",
            table: "Appointment",
            newName: "PetSitterServiceId");

        migrationBuilder.AlterColumn<DateTime>(
            name: "Time",
            table: "Schedule",
            type: "datetime2(0)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AddColumn<int>(
            name: "PetId",
            table: "Schedule",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "Type",
            table: "Schedule",
            type: "int",
            nullable: false,
            defaultValue: 0);

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

        migrationBuilder.AddColumn<decimal>(
            name: "Price",
            table: "Job",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Breed",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "UserName",
            table: "AppUser",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(256)",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "NormalizedUserName",
            table: "AppUser",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(256)",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "NormalizedEmail",
            table: "AppUser",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(256)",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "AppUser",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(256)",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "NormalizedName",
            table: "AppRole",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(256)",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "AppRole",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(256)",
            oldMaxLength: 256,
            oldNullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Address",
            table: "Appointment",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Note",
            table: "Appointment",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "PetId",
            table: "Appointment",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AlterColumn<string>(
            name: "PhoneNumber",
            table: "Application",
            type: "nvarchar(10)",
            maxLength: 10,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(15)",
            oldMaxLength: 15);

        migrationBuilder.CreateTable(
            name: "PetPhoto",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetId = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
            name: "PetSitter",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                IsVerified = table.Column<bool>(type: "bit", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Yoe = table.Column<int>(type: "int", nullable: false)
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
            name: "PetType",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetType", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PetVaccination",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetId = table.Column<int>(type: "int", nullable: false),
                Certification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                VaccineDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
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
            name: "TimeTable",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TimeTable", x => x.Id);
                table.ForeignKey(
                    name: "FK_TimeTable_Job_JobId",
                    column: x => x.JobId,
                    principalTable: "Job",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Transaction",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
            name: "PetSitterService",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetServiceId = table.Column<int>(type: "int", nullable: false),
                PetSitterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Rate = table.Column<float>(type: "real", nullable: true)
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
            name: "WorkHistory",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetSitterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ExperienceYears = table.Column<int>(type: "int", nullable: false),
                HaveTransport = table.Column<bool>(type: "bit", nullable: false),
                Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                NumPet = table.Column<int>(type: "int", nullable: false)
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
            name: "PetSubType",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetTypeId = table.Column<int>(type: "int", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SubName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetSubType", x => x.Id);
                table.ForeignKey(
                    name: "FK_PetSubType_PetType_PetTypeId",
                    column: x => x.PetTypeId,
                    principalTable: "PetType",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PetTypeJob",
            columns: table => new
            {
                JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetTypeId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PetTypeJob", x => new { x.JobId, x.PetTypeId });
                table.ForeignKey(
                    name: "FK_PetTypeJob_Job_JobId",
                    column: x => x.JobId,
                    principalTable: "Job",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_PetTypeJob_PetType_PetTypeId",
                    column: x => x.PetTypeId,
                    principalTable: "PetType",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PetSitterRate",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PetSitterServiceId = table.Column<int>(type: "int", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                Rate = table.Column<float>(type: "real", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true)
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

        migrationBuilder.CreateIndex(
            name: "IX_Schedule_PetId",
            table: "Schedule",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_Report_PetSitterId",
            table: "Report",
            column: "PetSitterId");

        migrationBuilder.CreateIndex(
            name: "IX_Pet_PetSubTypeId",
            table: "Pet",
            column: "PetSubTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_Appointment_PetId",
            table: "Appointment",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_Appointment_PetSitterServiceId",
            table: "Appointment",
            column: "PetSitterServiceId");

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
            name: "IX_PetSubType_PetTypeId",
            table: "PetSubType",
            column: "PetTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_PetSubType_SubName",
            table: "PetSubType",
            column: "SubName",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_PetType_Name",
            table: "PetType",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_PetTypeJob_PetTypeId",
            table: "PetTypeJob",
            column: "PetTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_PetVaccination_PetId",
            table: "PetVaccination",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_TimeTable_JobId",
            table: "TimeTable",
            column: "JobId");

        migrationBuilder.CreateIndex(
            name: "IX_Transaction_UserId",
            table: "Transaction",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_WorkHistory_PetSitterId",
            table: "WorkHistory",
            column: "PetSitterId");

        migrationBuilder.AddForeignKey(
            name: "FK_Appointment_AppUser_UserId",
            table: "Appointment",
            column: "UserId",
            principalTable: "AppUser",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Appointment_PetSitterService_PetSitterServiceId",
            table: "Appointment",
            column: "PetSitterServiceId",
            principalTable: "PetSitterService",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Appointment_Pet_PetId",
            table: "Appointment",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Breed_PetType_PetTypeId",
            table: "Breed",
            column: "PetTypeId",
            principalTable: "PetType",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Job_PetSitter_PetSitterId",
            table: "Job",
            column: "PetSitterId",
            principalTable: "PetSitter",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Pet_PetSubType_PetSubTypeId",
            table: "Pet",
            column: "PetSubTypeId",
            principalTable: "PetSubType",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Report_AppUser_UserId",
            table: "Report",
            column: "UserId",
            principalTable: "AppUser",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Report_PetSitter_PetSitterId",
            table: "Report",
            column: "PetSitterId",
            principalTable: "PetSitter",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Schedule_Pet_PetId",
            table: "Schedule",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id");
    }
}
