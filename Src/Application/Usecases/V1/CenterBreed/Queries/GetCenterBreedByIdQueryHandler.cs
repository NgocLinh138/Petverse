using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.CenterBreed;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.CenterBreed.Queries
{
    public sealed class GetCenterBreedByIdQueryHandler : IQueryHandler<Query.GetCenterBreedByIdQuery, Responses.CenterBreedResponse>
    {
        private readonly ICenterBreedRepository centerBreedRepository;
        private readonly IMapper mapper;

        public GetCenterBreedByIdQueryHandler(
            ICenterBreedRepository centerBreedRepository,
            IMapper mapper)
        {
            this.centerBreedRepository = centerBreedRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Responses.CenterBreedResponse>> Handle(Query.GetCenterBreedByIdQuery request, CancellationToken cancellationToken)
        { 
            var centerBreed = await centerBreedRepository.FindByIdAsync(request.Id, cancellationToken);
            if (centerBreed == null)
                return Result.Failure<Responses.CenterBreedResponse>("Không tìm thấy giống thú cưng.", StatusCodes.Status404NotFound);
            
            var response = mapper.Map<Responses.CenterBreedResponse>(centerBreed);
            return Result.Success(response, 200);
        }
    }
}
