using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.Junction;
using Domain.Entities.JunctionEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;
public sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public ApplicationDbContext() : base()
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
        builder.Ignore<IdentityUserRole<Guid>>();
    }

    // ================== Identity ==================
    public DbSet<AppUser> AppUser { get; set; }
    public DbSet<AppRole> AppRole { get; set; }

    // ================== Junction ==================
    public DbSet<ApplicationPetService> ApplicationPetService { get; set; }
    public DbSet<SpeciesJob> SpeciesJob { get; set; }

    // ================== Entities ==================

    public DbSet<Application> Application { get; set; }
    public DbSet<Appointment> Appointment { get; set; }
    public DbSet<Breed> Breed { get; set; }
    public DbSet<BreedAppointment> BreedAppointment { get; set; }
    public DbSet<CenterBreed> CenterBreed { get; set; }
    public DbSet<Certification> Certification { get; set; }
    public DbSet<Job> Job { get; set; }
    public DbSet<Transaction> Transaction { get; set; }
    public DbSet<Pet> Pet { get; set; }
    public DbSet<PetCenter> PetCenter { get; set; }
    public DbSet<AppointmentRate> AppointmentRate { get; set; }
    public DbSet<PetCenterService> PetCenterService { get; set; }
    public DbSet<PetService> PetService { get; set; }
    public DbSet<PetVaccinated> PetVaccinated { get; set; }
    public DbSet<Place> Place { get; set; }
    public DbSet<PetImage> PetImage { get; set; }
    public DbSet<Report> Report { get; set; }
    public DbSet<Schedule> Schedule { get; set; }
    public DbSet<ServiceAppointment> ServiceAppointment { get; set; }
    public DbSet<Species> Species { get; set; }
    public DbSet<Tracking> Tracking { get; set; }
    public DbSet<Vaccine> Vaccine { get; set; }
    public DbSet<VaccineRecommendation> VaccineRecommendation { get; set; }
}
