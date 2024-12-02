using AutoMapper;
using Contract.Abstractions.Shared;
using Contract.JsonConverters;
using Contract.Services.V1.PetCenter;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.Junction;
using static Contract.Services.V1.Application.Responses;
using static Contract.Services.V1.Appointment.Responses;
using static Contract.Services.V1.CenterBreed.Responses;
using static Contract.Services.V1.Job.Responses;
using static Contract.Services.V1.Pet.Responses;
using static Contract.Services.V1.PetCenter.Responses;
using static Contract.Services.V1.PetVaccinated.Responses;
using static Contract.Services.V1.Report.Responses;
using static Contract.Services.V1.Schedule.Responses;
using static Contract.Services.V1.Species.Responses;
using static Contract.Services.V1.Transaction.Responses;
using static Contract.Services.V1.User.Responses;

namespace Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap(typeof(PagedResult<>), typeof(PagedResult<>)).ConvertUsing(typeof(PagedResultConverter<,>));

        /*==================== V1 ====================*/

        #region Pet
        CreateMap<Pet, Contract.Services.V1.Pet.Responses.PetResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.SpeciesId, opt => opt.MapFrom(src => src.Breed.SpeciesId))
            .ForMember(dest => dest.BreedId, opt => opt.MapFrom(src => src.BreedId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.BirthDate)))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.Sterilized, opt => opt.MapFrom(src => src.Sterilized))
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.PetPhotos, opt => opt.MapFrom(src => src.Photos
                .Select(aps => new PetPhotoResponse
                {
                    PetPhotoId = aps.Id,
                    Url = aps.URL,
                    Type = aps.Type
                })))
             .ForMember(dest => dest.PetVaccinateds, opt => opt.MapFrom(src => src.PetVaccinateds
                .Select(aps => new PetVaccinatedByPetResponse
                {
                    Id = aps.Id,
                    Name = aps.Name,
                    Image = aps.Image,
                    DateVaccinated = DateTimeConverters.DateToString(aps.DateVaccinated)
                })));


        CreateMap<PagedResult<Pet>, PagedResult<Contract.Services.V1.Pet.Responses.PetResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        #endregion

        #region Application
        CreateMap<Domain.Entities.Application, ApplicationResponse>()
            .ForMember(dest => dest.Certifications, opt => opt.MapFrom(src => src.Certifications.Select(p => p.Image)))
            .ForMember(dest => dest.ApplicationPetServices, opt => opt.MapFrom(src => src.ApplicationPetServices
                .Select(aps => new ApplicationPetServiceResponse
                {
                    PetServiceId = aps.PetServiceId,
                })))
            .ReverseMap()
            .ForMember(dest => dest.ApplicationPetServices, opt => opt.MapFrom(src => src.ApplicationPetServices
                .Select(aps => new ApplicationPetService
                {
                    PetServiceId = aps.PetServiceId
                })));

        CreateMap<PagedResult<Domain.Entities.Application>, PagedResult<ApplicationResponse>>().ReverseMap();
        #endregion

        #region PetService
        CreateMap<PetService, Contract.Services.V1.PetService.Responses.PetServiceResponse>().ReverseMap();

        CreateMap<PagedResult<PetService>, PagedResult<Contract.Services.V1.PetService.Responses.PetServiceResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        #endregion

        #region PetCenterRate
        CreateMap<AppointmentRate, Contract.Services.V1.AppointmentRate.Responses.AppointmentRateResponse>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Appointment.User.FullName))
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Appointment.User.Avatar))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.CreatedDate)))
            .ReverseMap();

        CreateMap<PagedResult<AppointmentRate>, PagedResult<Contract.Services.V1.AppointmentRate.Responses.AppointmentRateResponse>>()
           .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        #endregion

        #region PetCenterService
        CreateMap<PetCenterService, Contract.Services.V1.PetCenterService.Responses.PetCenterServiceResponse>()
            .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => MapSchedule(src.Schedule)))
            .ReverseMap();

        CreateMap<PagedResult<PetCenterService>, PagedResult<Contract.Services.V1.PetCenterService.Responses.PetCenterServiceResponse>>()
           .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        #endregion

        #region Report
        CreateMap<Report, Contract.Services.V1.Report.Responses.ReportResponse>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.CreatedDate)))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.UpdatedDate)))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Appointment.UserId))  
            .ForMember(dest => dest.PetCenterId, opt => opt.MapFrom(src => src.Appointment.PetCenterId))    
            .ForMember(dest => dest.ReportImages, opt => opt.MapFrom(src => src.ReportImages
                .Select(aps => new ReportImageResponse
                {
                    Id = aps.Id,
                    URL = aps.URL,
                    Type = aps.Type
                }))).ReverseMap();

        CreateMap<PagedResult<Report>, PagedResult<Contract.Services.V1.Report.Responses.ReportResponse>>()
           .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<ReportImage, Contract.Services.V1.Report.Responses.ReportImageResponse>().ReverseMap();
        #endregion

        #region PetVaccinated
        CreateMap<PetVaccinated, PetVaccinatedResponse>()
            .ForMember(dest => dest.DateVaccinated, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.DateVaccinated)))
            .ReverseMap();

        CreateMap<PagedResult<PetVaccinated>, PagedResult<PetVaccinatedResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        #endregion

        #region Vaccine
        CreateMap<Vaccine, Contract.Services.V1.Vaccine.Responses.VaccineResponse>().ReverseMap();

        CreateMap<PagedResult<Vaccine>, PagedResult<Contract.Services.V1.Vaccine.Responses.VaccineResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        #endregion

        #region CenterBreed
        CreateMap<CenterBreed, Contract.Services.V1.CenterBreed.Responses.CenterBreedResponse>()
            .ForMember(dest => dest.IsDisable, opt => opt.MapFrom(src => src.IsDisabled))
            .ForMember(dest => dest.PetCenterName, opt => opt.MapFrom(src => src.PetCenter.Application.Name))
            .ForMember(dest => dest.SpeciesName, opt => opt.MapFrom(src => src.Species.Name))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.CenterBreedImages
                .Select(aps => new CenterBreedImageResponse
                {
                    CenterBreedImageId = aps.Id,
                    Image = aps.Image
                }))).ReverseMap();

        CreateMap<PagedResult<CenterBreed>, PagedResult<Contract.Services.V1.CenterBreed.Responses.CenterBreedResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        #endregion

        #region Schedule
        CreateMap<Schedule, Contract.Services.V1.Schedule.Responses.ScheduleResponse>()
            .ForMember(dest => dest.ScheduleId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Trackings, opt => opt.MapFrom(src => src.Trackings
                .Select(aps => new Contract.Services.V1.Schedule.Responses.TrackingResponse
                {
                    Id = aps.Id,
                    URL = aps.URL,
                    Type = aps.Type,
                    UploadedAt = DateTimeConverters.DateToString(aps.UploadedAt)
                })))
            .ReverseMap();
        #endregion

        #region Tracking
        CreateMap<Tracking, TrackingResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.URL, opt => opt.MapFrom(src => src.URL))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.UploadedAt, opt => opt.MapFrom(src => src.UploadedAt.ToString("dd/MM/yyyy")));
        #endregion

        #region Place
        CreateMap<Domain.Entities.Place, Contract.Services.V1.Place.Responses.PlaceResponse>()
            .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.SpeciesPlaces.Select(sp => sp.Species)))
            .ReverseMap();

        CreateMap<PagedResult<Domain.Entities.Place>, PagedResult<Contract.Services.V1.Place.Responses.PlaceResponse>>().ReverseMap();

        CreateMap<Domain.Entities.JunctionEntity.SpeciesPlace, Contract.Services.V1.Species.Responses.SpeciesResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SpeciesId));
        #endregion


        // Role
        CreateMap<AppRole, Contract.Services.V1.Role.Responses.RoleResponse>().ReverseMap();

        // User
        CreateMap<AppUser, UserResponse>().ReverseMap();
        CreateMap<AppUser, UserGetAllResponse>().ReverseMap();
        CreateMap<AppUser, UserGetByIdResponse>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.DateOfBirth)))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.CreatedDate)))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.UpdatedDate)))
            .ForMember(dest => dest.DeletedDate, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.DeletedDate)))
            .ReverseMap();

        #region PetCenter
        CreateMap<PetCenter, Responses.PetCenterResponse>().ReverseMap();
        CreateMap<PetCenter, PetCenterGetAllResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Application.UserId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Application.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Application.Address))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Application.PhoneNumber))
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Application.Avatar))
            .ForMember(dest => dest.Yoe, opt => opt.MapFrom(src => src.Application.Yoe))
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.PetCenterServices.Where(x => x.Rate != 0).Average(x => x.Rate)))
            .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.Job.SpeciesJobs.Select(x => x.Species.Name)))
            .ForMember(dest => dest.PetCenterServices, opt => opt.MapFrom(src => src.PetCenterServices.Select(x => x.PetService.Name)))
            .ReverseMap();

        CreateMap<PetCenterService, PetCenterServiceResponse>()
            .ForMember(dest => dest.PetCenterServiceId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PetService.Name))
            .ReverseMap();
        CreateMap<HashSet<PetCenterService>, ICollection<PetCenterServiceResponse>>().ReverseMap();
        CreateMap<PetCenter, Responses.PetCenterGetByIdResponse>().ReverseMap();


        CreateMap<Certification, Contract.Services.V1.PetCenter.Responses.CertificationResponse>().ReverseMap();
        #endregion
        // Job
        CreateMap<Job, JobResponse>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.CreatedDate)))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate.HasValue ? DateTimeConverters.DateToString(src.UpdatedDate) : null))
            .ReverseMap();
        // Species
        CreateMap<Species, SpeciesResponse>().ReverseMap();

        // Breed
        CreateMap<Breed, BreedResponse>().ReverseMap();


        #region Appointment
        CreateMap<Appointment, AppointmentResponse>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.CenterName, opt => opt.MapFrom(src => src.PetCenter.Application.Name))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.StartTime, "dd/MM/yyyy HH:mm")))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.EndTime, "dd/MM/yyyy HH:mm")))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.CreatedDate, "dd/MM/yyyy HH:mm")))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate.HasValue ? DateTimeConverters.DateToString(src.UpdatedDate, "dd/MM/yyyy HH:mm") : null))
            .ReverseMap();

        CreateMap<ServiceAppointment, ServiceAppointmentResponse>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.StartTime, "dd/MM/yyyy HH:mm")))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.EndTime, "dd/MM/yyyy HH:mm")))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap();

        CreateMap<BreedAppointment, BreedAppointmentResponse>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.StartTime, "dd/MM/yyyy HH:mm")))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.EndTime, "dd/MM/yyyy HH:mm")))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap();

        // Base mapping for common properties
        CreateMap<Appointment, AppointmentByIdResponse>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.CenterName, opt => opt.MapFrom(src => src.PetCenter.Application.Name))
            .ForMember(dest => dest.PetCenterServiceId, opt => opt.Ignore())
            .ForMember(dest => dest.CenterBreedId, opt => opt.Ignore())
            .ForMember(dest => dest.Schedules, opt => opt.Ignore())
            .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.Report))
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.AppointmentRate))
            .ForMember(dest => dest.SpeciesId, opt => opt.MapFrom(src => src.Pet.Breed.SpeciesId))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.StartTime)))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.EndTime)))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.CreatedDate)))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate.HasValue ? DateTimeConverters.DateToString(src.UpdatedDate) : null))
            .ReverseMap();
        // Mapping for BreedAppointment
        CreateMap<BreedAppointment, AppointmentByIdResponse>()
            .IncludeBase<Appointment, AppointmentByIdResponse>()
            .ForMember(dest => dest.CenterBreedId, opt => opt.MapFrom(src => src.CenterBreedId))
            .ForMember(dest => dest.PetCenterServiceId, opt => opt.Ignore())
            .ForMember(dest => dest.Schedules, opt => opt.Ignore());
        // Mapping for ServiceAppointment
        CreateMap<ServiceAppointment, AppointmentByIdResponse>()
            .IncludeBase<Appointment, AppointmentByIdResponse>()
            .ForMember(dest => dest.PetCenterServiceId, opt => opt.MapFrom(src => src.PetCenterServiceId))
            .ForMember(dest => dest.CenterBreedId, opt => opt.Ignore());


        CreateMap<Pet, Contract.Services.V1.Appointment.Responses.PetResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BreedId, opt => opt.MapFrom(src => src.BreedId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.BirthDate)))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.Sterilized, opt => opt.MapFrom(src => src.Sterilized))
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ReverseMap();
        #endregion

        // Payment
        CreateMap<Transaction, TransactionResponse>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTimeConverters.DateToString(src.CreatedDate)))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate.HasValue ? DateTimeConverters.DateToString(src.UpdatedDate) : null))
            .ReverseMap();

    }

    public class PagedResultConverter<TSource, TDestination> : ITypeConverter<PagedResult<TSource>, PagedResult<TDestination>>
    {
        public PagedResult<TDestination> Convert(PagedResult<TSource> source, PagedResult<TDestination> destination, ResolutionContext context)
        {
            var mappedItems = context.Mapper.Map<List<TDestination>>(source.Items);
            return new PagedResult<TDestination>(mappedItems, source.PageIndex, source.PageSize, source.TotalCount);
        }
    }

    public class DateFormatResolver : IValueResolver<Appointment, AppointmentResponse, string>
    {
        public string Resolve(Appointment source, AppointmentResponse destination, string destMember, ResolutionContext context)
        {
            return source.CreatedDate.ToString("dd/MM/yyyy");
        }
    }

    private static IEnumerable<Contract.Services.V1.PetCenterService.Responses.ScheduleService> MapSchedule(string schedule)
    {
        if (string.IsNullOrWhiteSpace(schedule))
            return Enumerable.Empty<Contract.Services.V1.PetCenterService.Responses.ScheduleService>();

        return schedule
            .Split(';', StringSplitOptions.RemoveEmptyEntries)
            .Select(entry =>
            {
                var parts = entry.Split('-', 2);
                return new Contract.Services.V1.PetCenterService.Responses.ScheduleService(
                    Time: parts.Length > 0 ? parts[0].Trim() : string.Empty,
                    Description: parts.Length > 1 ? parts[1].Trim() : string.Empty
                );
            });
    }

}