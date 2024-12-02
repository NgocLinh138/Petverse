using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Services.V1.Place;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities.JunctionEntity;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Contract.Services.V1.Species.Responses;

namespace Application.Usecases.V1.Place.Commands
{
    public sealed class CreatePlaceCommandHandler : ICommandHandler<Command.CreatePlaceCommand, Responses.PlaceResponse>
    {
        private readonly IPlaceRepository placeRepository;
        private readonly ISpeciesRepository speciesRepository;
        private readonly IBlobStorageService blobStorageService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreatePlaceCommandHandler(
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

        public async Task<Result<Responses.PlaceResponse>> Handle(Command.CreatePlaceCommand request, CancellationToken cancellationToken)
        {

            string? imageUrl = null;

            try
            {
                if (request.Image != null)
                {
                    var firstName = request.Name.Split(' ').FirstOrDefault() ?? "Place";
                    var nameImage = $"{firstName}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}{Path.GetExtension(request.Image.FileName)}";
                    imageUrl = await blobStorageService.UploadBlob(request.Image, nameImage, BlobFolder.places)
                        ?? throw new Exception("Không thể tải lên hình ảnh.");
                }

                if (request.SpeciesIds != null && request.SpeciesIds.Count > 0)
                {
                    var existingSpeciesIds = await speciesRepository.GetExistingSpeciesIdsAsync(request.SpeciesIds);

                    var invalidSpeciesIds = request.SpeciesIds.Except(existingSpeciesIds).ToList();

                    if (invalidSpeciesIds.Any())
                    {
                        return Result.Failure<Responses.PlaceResponse>(
                            $"Các SpeciesId không tồn tại: {string.Join(", ", invalidSpeciesIds)}",
                            StatusCodes.Status400BadRequest);
                    }
                }

                var place = new Domain.Entities.Place
                {
                    Type = request.Type,
                    Name = request.Name,
                    Lat = request.Lat,
                    Lng = request.Lng,
                    Address = request.Address,
                    Image = imageUrl,
                    Description = request.Description,
                    IsFree = request.IsFree
                };

                if (request.SpeciesIds != null && request.SpeciesIds.Count > 0)
                {
                    var speciesPlace = request.SpeciesIds.Select(x => new SpeciesPlace { SpeciesId = x }).ToList();
                    place.SpeciesPlaces = speciesPlace;
                }

                await placeRepository.AddAsync(place);
                await unitOfWork.SaveChangesAsync();

                var response = mapper.Map<Responses.PlaceResponse>(place);

                var speciesList = await speciesRepository
                    .FindAll(sp => place.SpeciesPlaces.Select(s => s.SpeciesId).Contains(sp.Id))
                    .ToListAsync(); response.Species = mapper.Map<List<SpeciesResponse>>(speciesList);

                return Result.Success(response, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    await blobStorageService.DeleteBlobSnapshotsAsync(imageUrl);
                }
                throw new Exception(ex.Message);
            }
        }
    }
}
