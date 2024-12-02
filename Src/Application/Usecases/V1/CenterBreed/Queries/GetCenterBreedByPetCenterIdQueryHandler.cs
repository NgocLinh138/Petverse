using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.CenterBreed;
using Domain.Abstractions.Repositories;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Usecases.V1.CenterBreed.Queries
{
    public sealed record GetCenterBreedByPetCenterIdQueryHandler : IQueryHandler<Query.GetCenterBreedByPetCenterIdQuery, PagedResult<Responses.CenterBreedResponse>>
    {
        private readonly ICenterBreedRepository centerBreedRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public GetCenterBreedByPetCenterIdQueryHandler(
            ICenterBreedRepository centerBreedRepository,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            this.centerBreedRepository = centerBreedRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<Result<PagedResult<Responses.CenterBreedResponse>>> Handle(Query.GetCenterBreedByPetCenterIdQuery request, CancellationToken cancellationToken)
        {
            var centerBreeds = centerBreedRepository.FindAll()
                .Where(x => x.PetCenterId == request.PetCenterId &&
                (string.IsNullOrEmpty(request.SearchTerm) || x.Name.Contains(request.SearchTerm)) &&
                (request.Status == null || x.Status == request.Status));

            if (centerBreeds == null || !centerBreeds.Any())
                return Result.Failure<PagedResult<Responses.CenterBreedResponse>>("Không tìm thấy giống thú cưng cho trung tâm này.", StatusCodes.Status404NotFound);

            var response = mapper.Map<List<Responses.CenterBreedResponse>>(centerBreeds);

            var result = PagedResult<Responses.CenterBreedResponse>.Create(
                response,
                request.PageIndex,
                request.PageSize,
                centerBreeds.Count());

            return Result.Success(result);
        }
    }
}
