using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class AppRole : IdentityRole<Guid>
{
    public string? Description { get; set; }

    public virtual ICollection<AppUser> Users { get; set; } = null!;
}
