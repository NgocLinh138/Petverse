using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;

namespace Contract.Services.V1.Appointment;

public static class Query
{
    public record GetAppointmentQuery(
        AppointmentType? Type,
        Guid? UserId,
        Guid? PetCenterId,
        AppointmentStatus? Status,
        int PageIndex,
        int PageSize) : IQuery<PagedResult<Responses.AppointmentResponse>>;

    public record GetAppointmentByIdQuery(
        Guid AppointmentId,
        AppointmentType Type) : IQuery<Responses.AppointmentByIdResponse>;

    public record GetBreedAppointmentByUserIdQuery(
        Guid UserId) : IQuery<Responses.BreedAppointmetByUserResponse>;
}
