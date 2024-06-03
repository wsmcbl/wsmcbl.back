namespace wsmcbl.back.config;

public static class ConvertUtc
{
    private static string zone = "Central Standard Time";
    internal static DateTime ConvertToUtc(this DateTime value)
    {
        var localDateTime = TimeZoneInfo.ConvertTime(value, TimeZoneInfo.FindSystemTimeZoneById(zone));
        return localDateTime.ToUniversalTime();
    }

    internal static DateTime ConvertFromUtc(this DateTime value)
    {
        var localDateTime = value.ToLocalTime();
        return TimeZoneInfo.ConvertTime(localDateTime, TimeZoneInfo.FindSystemTimeZoneById(zone));
    }
}