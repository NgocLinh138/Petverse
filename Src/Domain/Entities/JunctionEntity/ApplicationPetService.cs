namespace Domain.Entities.Junction;

public class ApplicationPetService
{
    public int ApplicationId { get; set; }
    public int PetServiceId { get; set; }

    public virtual Application Application { get; set; } = null!;
    public virtual PetService PetService { get; set; } = null!;
}
