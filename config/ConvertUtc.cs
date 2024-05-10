namespace wsmcbl.back.config;

public abstract class ConvertUtc
{
    internal static DateTime ConvertToUtc(DateTime value)
    {
        var localDateTime = TimeZoneInfo.ConvertTime(value, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
        return localDateTime.ToUniversalTime();
    }

    internal static DateTime ConvertFromUtc(DateTime value)
    {
        var localDateTime = value.ToLocalTime();
        return TimeZoneInfo.ConvertTime(localDateTime, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
    }
}