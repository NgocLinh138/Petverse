using Contract.Abstractions.Message;

namespace Contract.Services.V1.Dashboard;

public static class Query
{
    public record GetOverViewAdminQuery() : IQuery<Responses.OverviewAdminResponse>;
    public record GetLineChartAdminQuery() : IQuery<Responses.LineChartAdminResponse>;
    public record GetPipeChartAdminQuery() : IQuery<Responses.PipeChartAdminResponse>;
    public record GetBarChartAdminQuery() : IQuery<Responses.BarChartAdminResponse>;
    public record GetSummeryTableAdminQuery() : IQuery<Responses.SummeryTableAdminResponse>;

    public record GetOverViewManagerQuery() : IQuery<Responses.OverviewManagerResponse>;
    public record GetLineChartManagerQuery() : IQuery<Responses.LineChartManagerResponse>;
    public record GetBarChartManagerQuery() : IQuery<Responses.BarChartManagerResponse>;
}
