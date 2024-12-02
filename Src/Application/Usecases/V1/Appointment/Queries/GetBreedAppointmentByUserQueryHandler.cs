using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;
using Contract.JsonConverters;
using Contract.Services.V1.Appointment;
using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Usecases.V1.Appointment.Queries;

public sealed class GetBreedAppointmentByUserQueryHandler : IQueryHandler<Query.GetBreedAppointmentByUserIdQuery, Responses.BreedAppointmetByUserResponse>
{
    private readonly IBreedAppointmentRepository breedAppointmentRepository;

    public GetBreedAppointmentByUserQueryHandler(IBreedAppointmentRepository breedAppointmentRepository)
    {
        this.breedAppointmentRepository = breedAppointmentRepository;
    }

    public async Task<Result<Responses.BreedAppointmetByUserResponse>> Handle(Query.GetBreedAppointmentByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var breedAppointments = breedAppointmentRepository.FindAll(x => x.UserId == request.UserId && x.Status == AppointmentStatus.Completed);

            var breedAppointemntDatas = await breedAppointments.Select(x =>
                new Responses.BreedAppointemntData(
                    x.Id,
                    x.PetId,
                    x.CenterBreedId,
                    DateTimeConverters.DateToString(x.CreatedDate, "dd/MM/yyyy HH:mm"))).ToListAsync();
            var response = new Responses.BreedAppointmetByUserResponse(breedAppointemntDatas);

            return Result.Success(response);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }
}
