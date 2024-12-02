using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Pet;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Usecases.V1.Pet.Queries
{
    public sealed class GetPetByIdQueryHandler : IQueryHandler<Query.GetPetByIdQuery, Responses.PetResponse>
    {
        private readonly IPetRepository petRepossitory;
        private readonly IMapper mapper;

        public GetPetByIdQueryHandler(
            IPetRepository petRepossitory,
            IMapper mapper)
        {
            this.petRepossitory = petRepossitory;
            this.mapper = mapper;
        }

        public async Task<Result<Responses.PetResponse>> Handle(Query.GetPetByIdQuery request, CancellationToken cancellationToken)
        {
            var pet = await petRepossitory.FindAll()
                .Where(x => !x.IsDeleted)
                .Include(p => p.PetVaccinateds)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (pet == null)
                return Result.Failure<Responses.PetResponse>("Không tìm thấy thú cưng.", StatusCodes.Status404NotFound);

            var response = mapper.Map<Responses.PetResponse>(pet);

            return Result.Success(response);
        }
    }
}
