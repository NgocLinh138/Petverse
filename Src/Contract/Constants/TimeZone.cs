namespace Contract.Constants;
public static class TimeZones
{
    public static DateTime GetSoutheastAsiaTime()
    {
        return TimeZoneInfo.ConvertTimeFromUtc(
            DateTime.UtcNow,
            TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
    }
}

