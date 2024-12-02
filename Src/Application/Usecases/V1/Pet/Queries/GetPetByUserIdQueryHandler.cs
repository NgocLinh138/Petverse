using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Pet;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Usecases.V1.Pet.Queries
{
    public sealed class GetPetByUserIdQueryHandler : IQueryHandler<Query.GetPetByUserIdQuery, PagedResult<Responses.PetResponse>>
    {
        private readonly IPetRepository petRepository;
        private readonly IMapper mapper;

        public GetPetByUserIdQueryHandler(
            IPetRepository petRepository,
            IMapper mapper)
        {
            this.petRepository = petRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PagedResult<Responses.PetResponse>>> Handle(Query.GetPetByUserIdQuery request, CancellationToken cancellationToken)
        {
            var pets = await petRepository.FindAll()
                .Include(x => x.PetVaccinateds)
                .Where(x => x.UserId == request.UserId && !x.IsDeleted)
                .ToListAsync(cancellationToken);

            if (pets == null || !pets.Any())
                return Result.Failure<PagedResult<Responses.PetResponse>>("Không tìm thấy thú cưng cho người dùng này.", StatusCodes.Status404NotFound);

            var response = mapper.Map<List<Responses.PetResponse>>(pets);

            var result = PagedResult<Responses.PetResponse>.Create(
                response,
                request.PageIndex,
                request.PageSize,
                pets.Count());

            return Result.Success(result);
        }
    }
}
