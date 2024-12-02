using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetService;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.PetService.Queries
{
    public sealed class GetPetServiceByIdQueryHandler : IQueryHandler<Query.GetPetServiceByIdQuery, Responses.PetServiceResponse>
    {
        private readonly IPetServiceRepository petServiceRepository;
        private readonly IMapper mapper;

        public GetPetServiceByIdQueryHandler(
            IPetServiceRepository petServiceRepository,
            IMapper mapper)
        {
            this.petServiceRepository = petServiceRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Responses.PetServiceResponse>> Handle(Query.GetPetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var petService = await petServiceRepository.FindByIdAsync(request.Id, cancellationToken);
            if (petService == null)
                return Result.Failure<Responses.PetServiceResponse>("Không tìm thấy dịch vụ.", StatusCodes.Status404NotFound);

            var response = mapper.Map<Responses.PetServiceResponse>(petService);

            return Result.Success(response);
        }
    }
}
