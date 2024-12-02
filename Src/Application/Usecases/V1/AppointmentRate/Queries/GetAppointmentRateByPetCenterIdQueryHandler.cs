using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;
using Contract.Services.V1.AppointmentRate;
using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Usecases.V1.AppointmentRate.Queries
{
    public sealed class GetAppointmentRateByPetCenterIdQueryHandler : IQueryHandler<Query.GetAppointmentRateByPetCenterIdQuery, PagedResult<Responses.AppointmentRateResponse>>
    {
        private readonly IAppointmentRateRepository AppointmentRateRepository;
        private readonly IMapper mapper;

        public GetAppointmentRateByPetCenterIdQueryHandler(
            IAppointmentRateRepository AppointmentRateRepository,
            IMapper mapper)
        {
            this.AppointmentRateRepository = AppointmentRateRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PagedResult<Responses.AppointmentRateResponse>>> Handle(Query.GetAppointmentRateByPetCenterIdQuery request, CancellationToken cancellationToken)
        {
            var EventsQuery = AppointmentRateRepository.FindAll()
                .Include(x => x.Appointment)
                .Where(x => x.Appointment.PetCenterId == request.PetCenterId);

            var keySelector = GetSortProperty(request);

            var sortOrder = (request.SortOrder == 2) ? SortOrder.Descending : SortOrder.Ascending;

            EventsQuery = sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.AppointmentRate>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.AppointmentRateResponse>>(Events);

            return Result.Success(result);
        }

        public static Expression<Func<Domain.Entities.AppointmentRate, object>> GetSortProperty(Query.GetAppointmentRateByPetCenterIdQuery request)
            => request.SortColumn?.ToLower() switch
            {
                "rate" => e => e.Rate,
                _ => e => e.Id
            };
    }
}
