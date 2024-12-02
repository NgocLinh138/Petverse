using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.AppointmentRate;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.AppointmentRate.Queries
{
    public sealed class GetAppointmentRateByIdQueryHandler : IQueryHandler<Query.GetAppointmentRateByIdQuery, Responses.AppointmentRateResponse>
    {
        private readonly IAppointmentRateRepository AppointmentRateRepository;
        private readonly IMapper mapper;

        public GetAppointmentRateByIdQueryHandler(
            IAppointmentRateRepository AppointmentRateRepository,
            IMapper mapper)
        {
            this.AppointmentRateRepository = AppointmentRateRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Responses.AppointmentRateResponse>> Handle(Query.GetAppointmentRateByIdQuery request, CancellationToken cancellationToken)
        {
            var AppointmentRate = await AppointmentRateRepository.FindByIdAsync(request.Id, cancellationToken);
            if (AppointmentRate == null)
                return Result.Failure<Responses.AppointmentRateResponse>("Không tìm thấy đánh giá của cuộc hẹn.", StatusCodes.Status404NotFound);

            var response = mapper.Map<Responses.AppointmentRateResponse>(AppointmentRate);

            return Result.Success(response);
        }
    }
}
