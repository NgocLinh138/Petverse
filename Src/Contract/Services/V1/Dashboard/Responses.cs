using Contract.Enumerations;

namespace Contract.Services.V1.Dashboard;

public static class Responses
{
    public record OverviewAdminResponse(
        int NewUsers,
        int NewCompletedAppointments,
        int Jobs,
        decimal Revenue);

    public record LineChartAdminResponse(ICollection<LineChartAdminData> LineChart);
    public record LineChartAdminData(
        string Month,
        ICollection<ServiceAdminData> services
    );

    public record ServiceAdminData(
        string name,
        int total);


    public record BarChartAdminResponse(ICollection<BarChartAdminData> BarChart);

    public record BarChartAdminData(
        string Month,
        int Pending,
        int Completed);

    public record SummeryTableAdminResponse(
        List<TopPetCenter> TopCenters,
        List<RecentPayment> RecentPayments);

    public record PipeChartAdminResponse(
    int Processing,
    int Approved,
    int Rejected);

    public record TopPetCenter(
        string PetCenterName,
        string ServiceName,
        float Rate);

    public record RecentPayment(
        string? Title,
        DateTime Date,
        decimal Amount,
        TransactionStatus Status);

    // Manager
    public record OverviewManagerResponse(
        int TotalPet,
        int TotalPetCenter,
        int TotalPetCenterService,
        int ToTalCenterBreed);

    public record LineChartManagerResponse(ICollection<LineChartManagerData> LineChart);
    public record LineChartManagerData(
        string Month,
        int Pet,
        int PetCenter);

    public record BarChartManagerResponse(ICollection<BarChartManagerData> BarChart);

    public record BarChartManagerData(
        string Month,
        int Rejected,
        int Approved);
}
