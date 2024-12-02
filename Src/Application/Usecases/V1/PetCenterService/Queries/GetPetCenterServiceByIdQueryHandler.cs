using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetCenterService;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;
using static Contract.Services.V1.Job.Command;

namespace Application.Usecases.V1.PetCenterService.Queries
{
    public sealed class GetPetSitterServiceByIdQueryHandler : IQueryHandler<Query.GetPetCenterServiceByIdQuery, Responses.PetCenterServiceResponse>
    {
        private readonly IPetCenterServiceRepository petCenterServiceRepository;
        private readonly IMapper mapper;

        public GetPetSitterServiceByIdQueryHandler(
            IPetCenterServiceRepository petCenterServiceRepository,
            IMapper mapper)
        {
            this.petCenterServiceRepository = petCenterServiceRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Responses.PetCenterServiceResponse>> Handle(Query.GetPetCenterServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var petCenterService = await petCenterServiceRepository.FindByIdAsync(request.Id, cancellationToken);
            if (petCenterService == null)
                return Result.Failure<Responses.PetCenterServiceResponse>("Không tìm thấy dịch vụ trung tâm thú cưng.", StatusCodes.Status404NotFound);

            var response = mapper.Map<Responses.PetCenterServiceResponse>(petCenterService);

            return Result.Success(response);
        }
    }
}
