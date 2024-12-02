using Contract.Constants;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;
using static Contract.Services.V1.Dashboard.Responses;
using static Contract.Services.V1.PetCenter.Responses;

namespace Persistence.Repositories;

public class PetCenterRepository : RepositoryBase<PetCenter, Guid>, IPetCenterRepository
{
    private readonly ApplicationDbContext context;
    public PetCenterRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public void SoftDelete(PetCenter petSitter)
    {
        petSitter.DeletedDate = TimeZones.GetSoutheastAsiaTime();
        petSitter.IsDeleted = true;

        Update(petSitter);
    }

    public async Task<List<TopPetCenter>> GetTopPetCenterOfService()
    {
        var topPetCenters = new List<TopPetCenter>();

        var results = await context.Database
            .SqlQueryRaw<TopPetCenter>("EXEC GetTopPetCenterOfService")
            .ToListAsync();

        foreach (var result in results)
        {
            topPetCenters.Add(new TopPetCenter(
                result.PetCenterName,
                result.ServiceName,
                result.Rate
            ));
        }

        return topPetCenters;
    }

    public async Task<List<TopPetCenterData>> GetTop5PetCentersAsync(int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);


        var petCenterData = await context.PetCenter
            .Include(x => x.Application)
            .Include(X => X.Appointments).ThenInclude(x => x.AppointmentRate)
            .Where(pc => pc.Appointments.Any(a => a.CreatedDate >= startDate && a.CreatedDate <= endDate))
            .Select(pc => new
            {
                PetCenterId = pc.Id,
                Avatar = pc.Application.Avatar,
                PetCenterName = pc.Application.Name,
                Appointments = pc.Appointments
                    .Where(a => a.CreatedDate >= startDate && a.CreatedDate <= endDate && a.AppointmentRate != null)
            })
            .ToListAsync();

        var petCenterRates = petCenterData
            .Select(pc => new
            {
                pc.PetCenterId,
                pc.Avatar,
                pc.PetCenterName,
                AverageRate = pc.Appointments.Any()
                    ? pc.Appointments.Average(a => a.AppointmentRate.Rate)
                    : 0
            })
            .OrderByDescending(x => x.AverageRate)
            .Take(5)
            .ToList();


        var response = petCenterRates.Select(pr => new TopPetCenterData
        (
            PetCenterId: pr.PetCenterId,
            Avatar: pr.Avatar,
            Name: pr.PetCenterName,
            AverageRate: pr.AverageRate
        )).ToList();

        return response;
    }
}
