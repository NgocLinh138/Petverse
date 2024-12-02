using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Appointment;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Appointment.Commands;

public sealed class DeleteAppointmentCommandHandler : ICommandHandler<Command.DeleteAppointmentCommand>
{
    private readonly IAppointmentRepository appointmentRepository;
    public DeleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
    {
        this.appointmentRepository = appointmentRepository;
    }

    public async Task<Result> Handle(Command.DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {

        var BreedAppointment = await appointmentRepository.FindByIdAsync(request.Id);
        if (BreedAppointment == null)
            return Result.Failure("Không tìm thấy cuộc hẹn.", StatusCodes.Status404NotFound);

        appointmentRepository.Remove(BreedAppointment);
        return Result.Success(202);
    }
}