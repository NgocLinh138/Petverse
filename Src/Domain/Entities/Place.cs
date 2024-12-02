using Contract.Enumerations;
using Contract.Services.V1.Place;
using Domain.Abstractions.EntityBase;
using Domain.Entities.JunctionEntity;

namespace Domain.Entities;

public class Place : EntityBase<int>
{
    public PlaceType Type { get; set; }
    public string Name { get; set; }
    public string Lat { get; set; }
    public string Lng { get; set; }
    public string Address { get; set; }
    public string Image { get; set; }
    public string? Description { get; set; }
    public bool IsFree { get; set; }

    public virtual ICollection<SpeciesPlace> SpeciesPlaces { get; set; }

    public void Update(Command.UpdatePlaceCommand request, string? newImageUrl = null)
    {
        if (request.Type.HasValue)
            Type = request.Type.Value;

        if (!string.IsNullOrEmpty(request.Name))
            Name = request.Name;

        if (!string.IsNullOrEmpty(request.Lat))
            Lat = request.Lat;

        if (!string.IsNullOrEmpty(request.Lng))
            Lng = request.Lng;

        if (!string.IsNullOrEmpty(request.Address))
            Address = request.Address;

        if (!string.IsNullOrEmpty(request.Description))
            Description = request.Description;

        if (request.IsFree.HasValue)
            IsFree = request.IsFree.Value;

        if (!string.IsNullOrEmpty(newImageUrl))
            Image = newImageUrl;

        if (request.SpeciesIds != null && request.SpeciesIds.Count > 0)
        {
            SpeciesPlaces.Clear();

            foreach (var speciesId in request.SpeciesIds)
            {
                SpeciesPlaces.Add(new SpeciesPlace
                {
                    PlaceId = Id,
                    SpeciesId = speciesId
                });
            }
        }
    }
}
