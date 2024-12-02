using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Place;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Place.Queries
{
    public sealed class GetPlaceByIdQueryHandler : IQueryHandler<Query.GetPlaceByIdQuery, Responses.PlaceResponse>
    {
        private readonly IPlaceRepository placeRepository;
        private readonly IMapper mapper;

        public GetPlaceByIdQueryHandler(
            IPlaceRepository placeRepository,
            IMapper mapper)
        {
            this.placeRepository = placeRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Responses.PlaceResponse>> Handle(Query.GetPlaceByIdQuery request, CancellationToken cancellationToken)
        {
            var place = await placeRepository.FindByIdAsync(request.Id, cancellationToken);
            if (place == null)
                return Result.Failure<Responses.PlaceResponse>("Không tìm thấy địa điểm.", StatusCodes.Status404NotFound);

            var response = mapper.Map<Responses.PlaceResponse>(place);

            return Result.Success(response);
        }
    }
}
