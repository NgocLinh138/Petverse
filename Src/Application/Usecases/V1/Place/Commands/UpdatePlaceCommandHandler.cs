using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Services.V1.Place;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Entities.JunctionEntity;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Contract.Services.V1.Species.Responses;

namespace Application.Usecases.V1.Place.Commands
{
    public sealed class UpdatePlaceCommandHandler : ICommandHandler<Command.UpdatePlaceCommand, Responses.PlaceResponse>
    {
        private readonly IPlaceRepository placeRepository;
        private readonly ISpeciesRepository speciesRepository;
        private readonly IBlobStorageService blobStorageService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdatePlaceCommandHandler(
            IPlaceRepository placeRepository,
            ISpeciesRepository speciesRepository,
            IBlobStorageService blobStorageService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.placeRepository = placeRepository;
            this.speciesRepository = speciesRepository;
            this.blobStorageService = blobStorageService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.PlaceResponse>> Handle(Command.UpdatePlaceCommand request, CancellationToken cancellationToken)
        {
            var place = await placeRepository.FindByIdAsync(request.Id, cancellationToken); 
            if (place == null)
                return Result.Failure<Responses.PlaceResponse>("Không tìm thấy địa điểm.", StatusCodes.Status404NotFound);

            string? newImageUrl = null;

            try
            {
                if (request.Image != null)
                {
                    if (!string.IsNullOrEmpty(place.Image))
                    {
                        await blobStorageService.DeleteBlobSnapshotsAsync(place.Image);
                    }

                    var imageName = $"{request.Name}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";
                    newImageUrl = await blobStorageService.UploadBlob(request.Image, imageName, BlobFolder.places)
                        ?? throw new Exception("Không thể tải lên hình ảnh.");
                }

                place.Update(request, newImageUrl);

                if (request.SpeciesIds != null && request.SpeciesIds.Count > 0)
                {
                    var existingSpeciesIds = place.SpeciesPlaces.Select(sp => sp.SpeciesId).ToList();
                    var speciesToRemove = place.SpeciesPlaces.Where(sp => !request.SpeciesIds.Contains(sp.SpeciesId)).ToList();

                    foreach (var speciesPlace in speciesToRemove)
                    {
                        place.SpeciesPlaces.Remove(speciesPlace);
                    }

                    foreach (var speciesId in request.SpeciesIds)
                    {
                        if (!existingSpeciesIds.Contains(speciesId))
                        {
                            var speciesPlace = new SpeciesPlace
                            {
                                PlaceId = place.Id,
                                SpeciesId = speciesId
                            };

                            var species = await speciesRepository.FindByIdAsync(speciesId);
                            if (species != null)
                            {
                                speciesPlace.Species = species;
                            }

                            place.SpeciesPlaces.Add(speciesPlace);
                        }
                    }
                }


                placeRepository.Update(place);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                
                var response = mapper.Map<Responses.PlaceResponse>(place);

                var speciesList = await speciesRepository
                          .FindAll(sp => place.SpeciesPlaces.Select(s => s.SpeciesId).Contains(sp.Id))
                          .ToListAsync();

                response.Species = mapper.Map<List<SpeciesResponse>>(speciesList);

                return Result.Success(response, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(newImageUrl))
                {
                    await blobStorageService.DeleteBlobSnapshotsAsync(newImageUrl);
                }
                throw new Exception(ex.Message);
            }
        }
    }
}
