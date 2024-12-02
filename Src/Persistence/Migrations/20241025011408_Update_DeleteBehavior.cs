using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class Update_DeleteBehavior : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Application_AppUser_UserId",
            table: "Application");

        migrationBuilder.DropForeignKey(
            name: "FK_ApplicationPetService_Application_ApplicationId",
            table: "ApplicationPetService");

        migrationBuilder.DropForeignKey(
            name: "FK_AppUser_AppRole_RoleId",
            table: "AppUser");

        migrationBuilder.DropForeignKey(
            name: "FK_Breed_Species_SpeciesId",
            table: "Breed");

        migrationBuilder.DropForeignKey(
            name: "FK_BreedAppointment_CenterBreed_CenterBreedId",
            table: "BreedAppointment");

        migrationBuilder.DropForeignKey(
            name: "FK_Doctor_AppUser_UserId",
            table: "Doctor");

        migrationBuilder.DropForeignKey(
            name: "FK_Pet_Breed_BreedId",
            table: "Pet");

        migrationBuilder.DropForeignKey(
            name: "FK_PetCenter_Application_ApplicationId",
            table: "PetCenter");

        migrationBuilder.DropForeignKey(
            name: "FK_PetCenterRate_AppUser_UserId",
            table: "PetCenterRate");

        migrationBuilder.DropForeignKey(
            name: "FK_PetCenterService_PetCenter_PetCenterId",
            table: "PetCenterService");

        migrationBuilder.DropForeignKey(
            name: "FK_PetVaccinated_Pet_PetId",
            table: "PetVaccinated");

        migrationBuilder.DropForeignKey(
            name: "FK_Photo_Pet_PetId",
            table: "Photo");

        migrationBuilder.DropForeignKey(
            name: "FK_Report_PetCenter_UserId",
            table: "Report");

        migrationBuilder.DropForeignKey(
            name: "FK_ServiceAppointment_PetCenterService_PetCenterServiceId",
            table: "ServiceAppointment");

        migrationBuilder.DropForeignKey(
            name: "FK_ServiceAppointment_Pet_PetId",
            table: "ServiceAppointment");

        migrationBuilder.DropForeignKey(
            name: "FK_SpeciesJob_Job_JobId",
            table: "SpeciesJob");

        migrationBuilder.DropForeignKey(
            name: "FK_Tracking_Schedule_ScheduleId",
            table: "Tracking");

        migrationBuilder.DropForeignKey(
            name: "FK_UserChannel_Channel_ChannelId",
            table: "UserChannel");

        migrationBuilder.DropForeignKey(
            name: "FK_Vaccine_Species_SpeciesId",
            table: "Vaccine");

        migrationBuilder.DropForeignKey(
            name: "FK_VaccineRecommendation_Pet_PetId",
            table: "VaccineRecommendation");

        migrationBuilder.DropForeignKey(
            name: "FK_VaccineRecommendation_Vaccine_VaccineId",
            table: "VaccineRecommendation");

        migrationBuilder.DropForeignKey(
            name: "FK_Warning_Pet_PetId",
            table: "Warning");

        migrationBuilder.DropIndex(
            name: "IX_Doctor_UserId",
            table: "Doctor");

        migrationBuilder.DropIndex(
            name: "IX_BreedAppointment_CenterBreedId",
            table: "BreedAppointment");

        migrationBuilder.DropColumn(
            name: "UserId",
            table: "Doctor");

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "PetService",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(200)",
            oldMaxLength: 200,
            oldNullable: true);

        migrationBuilder.AddColumn<int>(
            name: "ApplicationId",
            table: "Doctor",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Application",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(200)",
            oldMaxLength: 200);

        migrationBuilder.AlterColumn<string>(
            name: "Address",
            table: "Application",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(100)",
            oldMaxLength: 100);

        migrationBuilder.CreateIndex(
            name: "IX_Doctor_ApplicationId",
            table: "Doctor",
            column: "ApplicationId",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_Application_AppUser_UserId",
            table: "Application",
            column: "UserId",
            principalTable: "AppUser",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_ApplicationPetService_Application_ApplicationId",
            table: "ApplicationPetService",
            column: "ApplicationId",
            principalTable: "Application",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AppUser_AppRole_RoleId",
            table: "AppUser",
            column: "RoleId",
            principalTable: "AppRole",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Breed_Species_SpeciesId",
            table: "Breed",
            column: "SpeciesId",
            principalTable: "Species",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_BreedAppointment_CenterBreed_PetId",
            table: "BreedAppointment",
            column: "PetId",
            principalTable: "CenterBreed",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Doctor_Application_ApplicationId",
            table: "Doctor",
            column: "ApplicationId",
            principalTable: "Application",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Pet_Breed_BreedId",
            table: "Pet",
            column: "BreedId",
            principalTable: "Breed",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_PetCenter_Application_ApplicationId",
            table: "PetCenter",
            column: "ApplicationId",
            principalTable: "Application",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_PetCenterRate_AppUser_UserId",
            table: "PetCenterRate",
            column: "UserId",
            principalTable: "AppUser",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_PetCenterService_PetCenter_PetCenterId",
            table: "PetCenterService",
            column: "PetCenterId",
            principalTable: "PetCenter",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_PetVaccinated_Pet_PetId",
            table: "PetVaccinated",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Photo_Pet_PetId",
            table: "Photo",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Report_PetCenter_UserId",
            table: "Report",
            column: "UserId",
            principalTable: "PetCenter",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_ServiceAppointment_PetCenterService_PetCenterServiceId",
            table: "ServiceAppointment",
            column: "PetCenterServiceId",
            principalTable: "PetCenterService",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_ServiceAppointment_Pet_PetId",
            table: "ServiceAppointment",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_SpeciesJob_Job_JobId",
            table: "SpeciesJob",
            column: "JobId",
            principalTable: "Job",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Tracking_Schedule_ScheduleId",
            table: "Tracking",
            column: "ScheduleId",
            principalTable: "Schedule",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_UserChannel_Channel_ChannelId",
            table: "UserChannel",
            column: "ChannelId",
            principalTable: "Channel",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Vaccine_Species_SpeciesId",
            table: "Vaccine",
            column: "SpeciesId",
            principalTable: "Species",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_VaccineRecommendation_Pet_PetId",
            table: "VaccineRecommendation",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_VaccineRecommendation_Vaccine_VaccineId",
            table: "VaccineRecommendation",
            column: "VaccineId",
            principalTable: "Vaccine",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Warning_Pet_PetId",
            table: "Warning",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Application_AppUser_UserId",
            table: "Application");

        migrationBuilder.DropForeignKey(
            name: "FK_ApplicationPetService_Application_ApplicationId",
            table: "ApplicationPetService");

        migrationBuilder.DropForeignKey(
            name: "FK_AppUser_AppRole_RoleId",
            table: "AppUser");

        migrationBuilder.DropForeignKey(
            name: "FK_Breed_Species_SpeciesId",
            table: "Breed");

        migrationBuilder.DropForeignKey(
            name: "FK_BreedAppointment_CenterBreed_PetId",
            table: "BreedAppointment");

        migrationBuilder.DropForeignKey(
            name: "FK_Doctor_Application_ApplicationId",
            table: "Doctor");

        migrationBuilder.DropForeignKey(
            name: "FK_Pet_Breed_BreedId",
            table: "Pet");

        migrationBuilder.DropForeignKey(
            name: "FK_PetCenter_Application_ApplicationId",
            table: "PetCenter");

        migrationBuilder.DropForeignKey(
            name: "FK_PetCenterRate_AppUser_UserId",
            table: "PetCenterRate");

        migrationBuilder.DropForeignKey(
            name: "FK_PetCenterService_PetCenter_PetCenterId",
            table: "PetCenterService");

        migrationBuilder.DropForeignKey(
            name: "FK_PetVaccinated_Pet_PetId",
            table: "PetVaccinated");

        migrationBuilder.DropForeignKey(
            name: "FK_Photo_Pet_PetId",
            table: "Photo");

        migrationBuilder.DropForeignKey(
            name: "FK_Report_PetCenter_UserId",
            table: "Report");

        migrationBuilder.DropForeignKey(
            name: "FK_ServiceAppointment_PetCenterService_PetCenterServiceId",
            table: "ServiceAppointment");

        migrationBuilder.DropForeignKey(
            name: "FK_ServiceAppointment_Pet_PetId",
            table: "ServiceAppointment");

        migrationBuilder.DropForeignKey(
            name: "FK_SpeciesJob_Job_JobId",
            table: "SpeciesJob");

        migrationBuilder.DropForeignKey(
            name: "FK_Tracking_Schedule_ScheduleId",
            table: "Tracking");

        migrationBuilder.DropForeignKey(
            name: "FK_UserChannel_Channel_ChannelId",
            table: "UserChannel");

        migrationBuilder.DropForeignKey(
            name: "FK_Vaccine_Species_SpeciesId",
            table: "Vaccine");

        migrationBuilder.DropForeignKey(
            name: "FK_VaccineRecommendation_Pet_PetId",
            table: "VaccineRecommendation");

        migrationBuilder.DropForeignKey(
            name: "FK_VaccineRecommendation_Vaccine_VaccineId",
            table: "VaccineRecommendation");

        migrationBuilder.DropForeignKey(
            name: "FK_Warning_Pet_PetId",
            table: "Warning");

        migrationBuilder.DropIndex(
            name: "IX_Doctor_ApplicationId",
            table: "Doctor");

        migrationBuilder.DropColumn(
            name: "ApplicationId",
            table: "Doctor");

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "PetService",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "UserId",
            table: "Doctor",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Application",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "Address",
            table: "Application",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.CreateIndex(
            name: "IX_Doctor_UserId",
            table: "Doctor",
            column: "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_BreedAppointment_CenterBreedId",
            table: "BreedAppointment",
            column: "CenterBreedId");

        migrationBuilder.AddForeignKey(
            name: "FK_Application_AppUser_UserId",
            table: "Application",
            column: "UserId",
            principalTable: "AppUser",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_ApplicationPetService_Application_ApplicationId",
            table: "ApplicationPetService",
            column: "ApplicationId",
            principalTable: "Application",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_AppUser_AppRole_RoleId",
            table: "AppUser",
            column: "RoleId",
            principalTable: "AppRole",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Breed_Species_SpeciesId",
            table: "Breed",
            column: "SpeciesId",
            principalTable: "Species",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_BreedAppointment_CenterBreed_CenterBreedId",
            table: "BreedAppointment",
            column: "CenterBreedId",
            principalTable: "CenterBreed",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Doctor_AppUser_UserId",
            table: "Doctor",
            column: "UserId",
            principalTable: "AppUser",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Pet_Breed_BreedId",
            table: "Pet",
            column: "BreedId",
            principalTable: "Breed",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_PetCenter_Application_ApplicationId",
            table: "PetCenter",
            column: "ApplicationId",
            principalTable: "Application",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_PetCenterRate_AppUser_UserId",
            table: "PetCenterRate",
            column: "UserId",
            principalTable: "AppUser",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_PetCenterService_PetCenter_PetCenterId",
            table: "PetCenterService",
            column: "PetCenterId",
            principalTable: "PetCenter",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_PetVaccinated_Pet_PetId",
            table: "PetVaccinated",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Photo_Pet_PetId",
            table: "Photo",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Report_PetCenter_UserId",
            table: "Report",
            column: "UserId",
            principalTable: "PetCenter",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_ServiceAppointment_PetCenterService_PetCenterServiceId",
            table: "ServiceAppointment",
            column: "PetCenterServiceId",
            principalTable: "PetCenterService",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_ServiceAppointment_Pet_PetId",
            table: "ServiceAppointment",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_SpeciesJob_Job_JobId",
            table: "SpeciesJob",
            column: "JobId",
            principalTable: "Job",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Tracking_Schedule_ScheduleId",
            table: "Tracking",
            column: "ScheduleId",
            principalTable: "Schedule",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_UserChannel_Channel_ChannelId",
            table: "UserChannel",
            column: "ChannelId",
            principalTable: "Channel",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Vaccine_Species_SpeciesId",
            table: "Vaccine",
            column: "SpeciesId",
            principalTable: "Species",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_VaccineRecommendation_Pet_PetId",
            table: "VaccineRecommendation",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_VaccineRecommendation_Vaccine_VaccineId",
            table: "VaccineRecommendation",
            column: "VaccineId",
            principalTable: "Vaccine",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Warning_Pet_PetId",
            table: "Warning",
            column: "PetId",
            principalTable: "Pet",
            principalColumn: "Id");
    }
}
