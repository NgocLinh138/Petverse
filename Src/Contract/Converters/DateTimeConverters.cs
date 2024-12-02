using System.Globalization;

namespace Contract.JsonConverters;

public static class DateTimeConverters
{
    private const string DateTimeFormat = "dd/MM/yyyy";
    public static string DateToString(DateTime? dateTime)
    {
        return DateToString(dateTime, DateTimeFormat);
    }

    public static string DateToString(DateTime? dateTime, string? format = DateTimeFormat)
    {
        if (dateTime == null)
            return string.Empty;

        var result = dateTime.Value.ToString(format);
        return result;
    }

    public static DateTime? StringToDate(string dateTimeString, string? format = DateTimeFormat)
    {
        if (string.IsNullOrEmpty(dateTimeString))
            return null;

        // Kiểm tra trường hợp định dạng đầy đủ với thời gian
        if (DateTime.TryParseExact(dateTimeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
        {
            return dateTime;
        }

        return null;
    }
}
