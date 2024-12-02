using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Vaccine;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Vaccine.Queries
{
    public sealed class GetVaccineByIdQueryHandler : IQueryHandler<Query.GetVaccineByIdQuery, Responses.VaccineResponse>
    {
        private readonly IVaccineRepository vaccineRepository;
        private readonly IMapper mapper;

        public GetVaccineByIdQueryHandler(
            IVaccineRepository vaccineRepository,
            IMapper mapper)
        {
            this.vaccineRepository = vaccineRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Responses.VaccineResponse>> Handle(Query.GetVaccineByIdQuery request, CancellationToken cancellationToken)
        {
            var vaccine = await vaccineRepository.FindByIdAsync(request.Id, cancellationToken);
            if (vaccine == null)
                return Result.Failure<Responses.VaccineResponse>("Không tìm thấy vaccine.", StatusCodes.Status404NotFound);

            var response = mapper.Map<Responses.VaccineResponse>(vaccine);
            return Result.Success(response);
        }
    }
}
