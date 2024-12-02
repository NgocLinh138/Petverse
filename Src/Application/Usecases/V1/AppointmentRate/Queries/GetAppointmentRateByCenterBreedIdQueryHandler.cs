using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.AppointmentRate;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Usecases.V1.AppointmentRate.Queries
{
    public sealed class GetAppointmentRateByCenterBreedIdQueryHandler : IQueryHandler<Query.GetAppointmentRateByCenterBreedIdQuery, PagedResult<Responses.AppointmentRateResponse>>
    {
        private readonly IAppointmentRateRepository AppointmentRateRepository;
        private readonly IMapper mapper;

        public GetAppointmentRateByCenterBreedIdQueryHandler(
            IAppointmentRateRepository AppointmentRateRepository,
            IMapper mapper)
        {
            this.AppointmentRateRepository = AppointmentRateRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PagedResult<Responses.AppointmentRateResponse>>> Handle(Query.GetAppointmentRateByCenterBreedIdQuery request, CancellationToken cancellationToken)
        {
            var EventsQuery = AppointmentRateRepository.FindAll().Include(x => x.Appointment)
                .Where(x => x.Appointment is BreedAppointment && ((BreedAppointment)x.Appointment).CenterBreedId == request.CenterBreedId);

            var keySelector = GetSortProperty(request);

            EventsQuery = request.SortOrder == 2
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.AppointmentRate>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.AppointmentRateResponse>>(Events);

            return Result.Success(result);
        }

        public static Expression<Func<Domain.Entities.AppointmentRate, object>> GetSortProperty(Query.GetAppointmentRateByCenterBreedIdQuery request)
            => request.SortColumn?.ToLower() switch
            {
                "rate" => e => e.Rate,
                _ => e => e.Id
            };
    }
}
