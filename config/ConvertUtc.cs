namespace wsmcbl.back.config;

public abstract class ConvertUtc
{
    private static string zone = "Central Standard Time";
    internal static DateTime ConvertToUtc(DateTime value)
    {
        var localDateTime = TimeZoneInfo.ConvertTime(value, TimeZoneInfo.FindSystemTimeZoneById(zone));
        return localDateTime.ToUniversalTime();
    }

    internal static DateTime ConvertFromUtc(DateTime value)
    {
        var localDateTime = value.ToLocalTime();
        return TimeZoneInfo.ConvertTime(localDateTime, TimeZoneInfo.FindSystemTimeZoneById(zone));
    }
}