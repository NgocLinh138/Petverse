using Domain.Abstractions.Repositories.Base;
using Domain.Entities;
using static Contract.Services.V1.Dashboard.Responses;

namespace Domain.Abstractions.Repositories;

public interface ITransactionRepository : IRepositoryBase<Transaction, Guid>
{
    Task<BarChartAdminResponse> GetMonthlyTransactionSummaryAsync();
    Task<LineChartAdminResponse> GetMonthlyServiceAppointmentSummaryAsync();
    Task<BarChartManagerResponse> GetMonthlyReportSummaryAsync();
    Task<LineChartManagerResponse> GetMonthlyPetAndPetCenterSummaryAsync();
    IQueryable<Transaction> GetRecentlyCompletedTransaction();
}
