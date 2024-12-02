using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.AppointmentRate;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Usecases.V1.AppointmentRate.Commands
{
    public sealed class UpdateAppointmentRateCommandHandler : ICommandHandler<Command.UpdateAppointmentRateCommand, Responses.AppointmentRateResponse>
    {
        private readonly IAppointmentRateRepository AppointmentRateRepository;
        private readonly IPetCenterServiceRepository petCenterServiceRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly ICenterBreedRepository centerBreedRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateAppointmentRateCommandHandler(
            IAppointmentRateRepository AppointmentRateRepository,
            IPetCenterServiceRepository petCenterServiceRepository,
            IAppointmentRepository appointmentRepository,
            ICenterBreedRepository centerBreedRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.AppointmentRateRepository = AppointmentRateRepository;
            this.petCenterServiceRepository = petCenterServiceRepository;
            this.appointmentRepository = appointmentRepository;
            this.centerBreedRepository = centerBreedRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.AppointmentRateResponse>> Handle(Command.UpdateAppointmentRateCommand request, CancellationToken cancellationToken)
        {
            var AppointmentRate = await AppointmentRateRepository.FindByIdAsync(request.Id, cancellationToken);
            if (AppointmentRate == null)
                return Result.Failure<Responses.AppointmentRateResponse>("Không tìm thấy đánh giá của cuộc hẹn.", StatusCodes.Status404NotFound);

            AppointmentRate.Update(request);
            AppointmentRateRepository.Update(AppointmentRate);
            await unitOfWork.SaveChangesAsync();

            await UpdateRateAfterUpdateAsync(AppointmentRate.AppointmentId, cancellationToken);

            var response = mapper.Map<Responses.AppointmentRateResponse>(AppointmentRate);

            return Result.Success(response, 202);
        }

        private async Task UpdatePetCenterServiceRateAsync(Guid petCenterId, int serviceId, CancellationToken cancellationToken)
        {
            var petCenterService = await petCenterServiceRepository.FindSingleAsync(x => x.PetCenterId == petCenterId && x.Id == serviceId, cancellationToken);

            if (petCenterService != null)
            {
                var allRates = await AppointmentRateRepository.FindAll().ToListAsync(cancellationToken);

                var rates = allRates
                    .Where(x => x.Appointment is ServiceAppointment sa && sa.PetCenterServiceId == petCenterService.Id)
                    .Select(x => x.Rate).ToList();

                petCenterService.Rate = rates.Any() ? rates.Average() : 0;
                petCenterServiceRepository.Update(petCenterService);
                await unitOfWork.SaveChangesAsync();
            }
        }

        private async Task UpdateCenterBreedRateAsync(int centerBreedId, CancellationToken cancellationToken)
        {
            var centerBreed = await centerBreedRepository.FindByIdAsync(centerBreedId, cancellationToken);

            if (centerBreed != null)
            {
                var allRates = await AppointmentRateRepository.FindAll().ToListAsync(cancellationToken);

                var rates = allRates
                    .Where(x => x.Appointment is BreedAppointment sa && sa.CenterBreedId == centerBreedId)
                    .Select(x => x.Rate).ToList();

                centerBreed.Rate = rates.Any() ? rates.Average() : 0;
                centerBreedRepository.Update(centerBreed);
                await unitOfWork.SaveChangesAsync();
            }
        }

        private async Task UpdateRateAfterUpdateAsync(Guid appointmentId, CancellationToken cancellationToken)
        {
            var appointment = await appointmentRepository.FindByIdAsync(appointmentId, cancellationToken);
            if (appointment == null) return;

            if (appointment is ServiceAppointment serviceAppointment)
            {
                await UpdatePetCenterServiceRateAsync(appointment.PetCenterId, serviceAppointment.PetCenterServiceId, cancellationToken);
            }
            else if (appointment is BreedAppointment breedAppointment)
            {
                await UpdateCenterBreedRateAsync(breedAppointment.CenterBreedId, cancellationToken);
            }
        }
    }
}
