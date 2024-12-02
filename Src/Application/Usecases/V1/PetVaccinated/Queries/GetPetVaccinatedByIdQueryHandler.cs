using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetVaccinated;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.PetVaccinated.Queries
{
    public sealed class GetPetVaccinatedByIdQueryHandler : IQueryHandler<Query.GetPetVaccinatedByIdQuery, Responses.PetVaccinatedResponse>
    {
        private readonly IPetVaccinatedRepository petVaccinatedRepository;
        private readonly IMapper mapper;

        public GetPetVaccinatedByIdQueryHandler(
            IPetVaccinatedRepository petVaccinatedRepository,
            IMapper mapper)
        {
            this.petVaccinatedRepository = petVaccinatedRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Responses.PetVaccinatedResponse>> Handle(Query.GetPetVaccinatedByIdQuery request, CancellationToken cancellationToken)
        {
            var petVaccinated = await petVaccinatedRepository.FindByIdAsync(request.Id, cancellationToken);
            if (petVaccinated == null)
                return Result.Failure<Responses.PetVaccinatedResponse>("Không tìm thấy thông tin đã tiêm phòng của thú cưng.", StatusCodes.Status404NotFound);

            var response = mapper.Map<Responses.PetVaccinatedResponse>(petVaccinated);
            return Result.Success(response);
        }
    }
}
