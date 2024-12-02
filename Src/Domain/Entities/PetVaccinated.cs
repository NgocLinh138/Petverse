using Contract.JsonConverters;
using Contract.Services.V1.PetVaccinated;
using Domain.Abstractions.EntityBase;

namespace Domain.Entities
{
    public class PetVaccinated : EntityBase<int>
    {
        public int PetId { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public DateTime DateVaccinated { get; set; }

        public virtual Pet Pet { get; set; } = null!;

        public void Update(Command.UpdatePetVaccinatedCommand request)
        {
            Name = request.Name;
            DateVaccinated = DateTimeConverters.StringToDate(request.DateVaccinated).Value;
        }
    }
}
