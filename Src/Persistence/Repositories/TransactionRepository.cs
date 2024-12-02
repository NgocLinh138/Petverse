using Contract.Constants;
using Contract.Enumerations;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;
using static Contract.Services.V1.Dashboard.Responses;

namespace Persistence.Repositories;

public class TransactionRepository : RepositoryBase<Transaction, Guid>, ITransactionRepository
{
    private readonly ApplicationDbContext context;
    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public IQueryable<Transaction> GetRecentlyCompletedTransaction()
    {
        var recentlycompletedTransaction = FindAll(x => x.Status == TransactionStatus.Completed);

        recentlycompletedTransaction = recentlycompletedTransaction.OrderByDescending(x => x.CreatedDate).Take(5);
        return recentlycompletedTransaction;
    }

    //Get MontlyTransactions InLast12MonthsAsync
    public async Task<BarChartAdminResponse> GetMonthlyTransactionSummaryAsync()
    {
        var currentMonth = new DateTime(TimeZones.GetSoutheastAsiaTime().Year, TimeZones.GetSoutheastAsiaTime().Month, 1);
        var startDate = currentMonth.AddMonths(-11);

        var transactions = await context.Transaction
            .Where(p => p.CreatedDate >= startDate)
            .GroupBy(p => new { p.CreatedDate.Year, p.CreatedDate.Month })
            .Select(g => new
            {
                Month = g.Key.Year * 100 + g.Key.Month,
                Pending = g.Count(x => x.Status == TransactionStatus.Pending),
                Completed = g.Count(x => x.Status == TransactionStatus.Completed),
            })
            .OrderBy(x => x.Month)
            .ToListAsync();

        var response = transactions.Select(x => new BarChartAdminData(x.Month.ToString(), x.Pending, x.Completed)).ToList();

        return new BarChartAdminResponse(response);
    }

    public async Task<LineChartAdminResponse> GetMonthlyServiceAppointmentSummaryAsync()
    {
        var currentMonth = new DateTime(TimeZones.GetSoutheastAsiaTime().Year, TimeZones.GetSoutheastAsiaTime().Month, 1);
        var startDate = currentMonth.AddMonths(-11);

        var serviceAppointments = await context.ServiceAppointment
            .Where(sa => sa.CreatedDate >= startDate) // Lọc 12 tháng trước
            .GroupBy(sa => new { sa.CreatedDate.Year, sa.CreatedDate.Month, sa.PetCenterService.PetService.Name }) // Nhóm theo tháng và tên dịch vụ
            .Select(g => new
            {
                Month = g.Key.Year * 100 + g.Key.Month, // Chuẩn hóa tháng (YYYYMM)
                ServiceName = g.Key.Name,       // Tên dịch vụ
                AppointmentCount = g.Count()    // Số lượng appointment
            })
            .OrderBy(x => x.Month) // Sắp xếp theo tháng
            .ThenBy(x => x.ServiceName) // Sắp xếp theo tên dịch vụ
            .ToListAsync();

        // Chuyển đổi dữ liệu sang LineChartData
        var lineChartData = serviceAppointments
            .GroupBy(x => x.Month) // Nhóm theo từng tháng
            .Select(g => new LineChartAdminData(
                Month: g.Key.ToString(),
                services: g.Select(x => new ServiceAdminData(
                    name: x.ServiceName,
                    total: x.AppointmentCount
                )).ToList()
            )).ToList();

        return new LineChartAdminResponse(lineChartData);
    }

    public async Task<BarChartManagerResponse> GetMonthlyReportSummaryAsync()
    {
        var currentMonth = new DateTime(TimeZones.GetSoutheastAsiaTime().Year, TimeZones.GetSoutheastAsiaTime().Month, 1);
        var startDate = currentMonth.AddMonths(-11);

        var reports = await context.Report
            .Where(p => p.CreatedDate >= startDate)
            .GroupBy(p => new { p.CreatedDate.Year, p.CreatedDate.Month })
            .Select(g => new
            {
                Month = g.Key.Year * 100 + g.Key.Month,
                Rejected = g.Count(x => x.Status == ReportStatus.Rejected),
                Approved = g.Count(x => x.Status == ReportStatus.Approved),
            })
            .OrderBy(x => x.Month)
            .ToListAsync();

        var response = reports.Select(x => new BarChartManagerData(x.Month.ToString(), x.Rejected, x.Approved)).ToList();

        return new BarChartManagerResponse(response);
    }

    public async Task<LineChartManagerResponse> GetMonthlyPetAndPetCenterSummaryAsync()
    {
        var currentMonth = new DateTime(TimeZones.GetSoutheastAsiaTime().Year, TimeZones.GetSoutheastAsiaTime().Month, 1);
        var startDate = currentMonth.AddMonths(-11);

        //var pets = context.Pet
        //    .Where(p => p.CreatedDate >= startDate)
        //    .GroupBy(p => new { p.CreatedDate.Year, p.CreatedDate.Month })
        //    .Select(g => new
        //    {
        //        Month = g.Key.Year * 100 + g.Key.Month,
        //        Pet = g.Count(),
        //        PetCenter = 0,
        //    })
        //    .OrderBy(x => x.Month)
        //    .AsEnumerable();

        //var petCenters = context.PetCenter
        //    .Where(p => p.CreatedDate >= startDate)
        //    .GroupBy(p => new { p.CreatedDate.Year, p.CreatedDate.Month })
        //    .Select(g => new
        //    {
        //        Month = g.Key.Year * 100 + g.Key.Month,
        //        Pet = 0,
        //        PetCenter = g.Count()
        //    })
        //    .OrderBy(x => x.Month)
        //    .AsEnumerable();

        //var response = pets
        //   .Union(petCenters)
        //   .GroupBy(x => x.Month)
        //   .Select(g => new LineChartManagerData(
        //       Month: g.Key.ToString(),
        //       Pet: g.FirstOrDefault(x => x.Pet > 0)?.Pet ?? 0,          // Nếu có Pet thì lấy, nếu không có thì gán là 0
        //       PetCenter: g.FirstOrDefault(x => x.PetCenter > 0)?.PetCenter ?? 0  // Nếu có PetCenter thì lấy, nếu không có thì gán là 0
        //   ))
        //   .ToList();

        var response = context.Pet
            .Where(p => p.CreatedDate >= startDate)
            .GroupBy(p => new { p.CreatedDate.Year, p.CreatedDate.Month })
            .Select(g => new
            {
                Month = g.Key.Year * 100 + g.Key.Month,
                Pet = g.Count(),
                PetCenter = 0,
            })
            .Union(
                 context.PetCenter
                .Where(p => p.CreatedDate >= startDate)
                .GroupBy(p => new { p.CreatedDate.Year, p.CreatedDate.Month })
                .Select(g => new
                {
                    Month = g.Key.Year * 100 + g.Key.Month,
                    Pet = 0,
                    PetCenter = g.Count()
                }))
            .OrderBy(x => x.Month)
            .Select(g => new LineChartManagerData(
                 g.Month.ToString(),
                 g.Pet,  // Tổng số Pet trong tháng
                 g.PetCenter  // Tổng số PetCenter trong tháng
                ))
            .ToList();

        return new LineChartManagerResponse(response);
    }

}
