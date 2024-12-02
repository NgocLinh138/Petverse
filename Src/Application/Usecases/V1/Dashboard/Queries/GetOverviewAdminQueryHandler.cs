using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Enumerations;
using Contract.Services.V1.Dashboard;
using Domain.Abstractions.Repositories;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Usecases.V1.Dashboard.Queries;

public sealed class GetOverviewAdminQueryHandler : IQueryHandler<Query.GetOverViewAdminQuery, Responses.OverviewAdminResponse>
{
    private readonly UserManager<AppUser> userManager;
    private readonly IAppointmentRepository appointmentRepository;
    private readonly IJobRepository jobRepository;
    private readonly ITransactionRepository paymentRepository;
    private readonly DateTime ThisMonth;

    public GetOverviewAdminQueryHandler(UserManager<AppUser> userManager, IAppointmentRepository appointmentRepository, IJobRepository jobRepository, ITransactionRepository paymentRepository)
    {
        this.userManager = userManager;
        this.appointmentRepository = appointmentRepository;
        this.jobRepository = jobRepository;
        this.paymentRepository = paymentRepository;
        ThisMonth = new DateTime(TimeZones.GetSoutheastAsiaTime().Year, TimeZones.GetSoutheastAsiaTime().Month, 1);
    }

    public async Task<Result<Responses.OverviewAdminResponse>> Handle(Query.GetOverViewAdminQuery request, CancellationToken cancellationToken)
    {
        try
        {
            int NewUsers = await GetNewUserInMonth();
            int NewCompletedAppointments = await GetNewCompletedAppointments();
            int Jobs = await GetAllJob();
            decimal Revenue = await GetRevenueThisMonth();

            var response = new Responses.OverviewAdminResponse(
                NewUsers,
                NewCompletedAppointments,
                Jobs,
                Revenue);

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private async Task<decimal> GetRevenueThisMonth()
    {
        var ListPaymentThisMonth = await paymentRepository.FindAll(x =>
                    x.Status == TransactionStatus.Completed
                    && x.CreatedDate >= ThisMonth).ToListAsync();

        decimal RevenueThisMonth = ListPaymentThisMonth.Sum(x => x.Amount);
        return RevenueThisMonth;
    }

    private async Task<int> GetNewCompletedAppointments()
    {
        var count = await appointmentRepository.FindAll(x =>
                     x.Status == AppointmentStatus.Completed
                     && x.CreatedDate >= ThisMonth).CountAsync();

        return count;
    }

    private async Task<int> GetAllJob()
    {
        var count = await jobRepository.FindAll().CountAsync();
        return count;
    }

    private async Task<int> GetNewUserInMonth()
    {
        var NewUsers = await userManager.Users
            .Where(user => user.CreatedDate >= ThisMonth)
            .CountAsync();

        return NewUsers;
    }
}
