using System.Globalization;
using System.Text;

namespace wsmcbl.src.utilities;

public static class Utility
{
    public static DateTime toUTC6(this DateTime? datetime) => ((DateTime)datetime!).toUTC6();
    
    public static DateTime toUTC6(this DateTime datetime)
    {
        var timeZoneUTC6 = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(datetime, timeZoneUTC6);
    }

    public static string toStringUtc6(this DateTime? datetime, bool withDayName = false)
        => toStringUtc6((DateTime)datetime!, withDayName);
    
    public static string toStringUtc6(this DateTime datetime, bool withDayName = false)
    {
        var culture = new CultureInfo("es-ES")
        {
            DateTimeFormat =
            {
                AMDesignator = "am",
                PMDesignator = "pm"
            }
        };

        var dayFormat = withDayName ? "dddd" : "ddd.";
        return datetime.toUTC6().ToString($"{dayFormat} dd/MMM/yyyy, h:mm tt", culture);
    }
    
    public static string toDateUtc6(this DateTime datetime)
    {
        return datetime.toUTC6().ToString("dd/MMMM/yyyy", new CultureInfo("es-ES"));
    }
    
    public static string ReplaceInLatexFormat(this string text, string oldValue, string? newValue)
    {
        return text.Replace(oldValue, newValue.ReplaceLatexSpecialSymbols());
    }
    
    private static readonly string[] specialSymbols = ["$", "€", "£", "¥", "#", "%", "&", "_", "{", "}"];
    public static string ReplaceLatexSpecialSymbols(this string? text)
    {
        if (text == null)
            return string.Empty;
        
        var sb = new StringBuilder();
        foreach (var c in text)
        {
            if (c == '\\')
            {
                sb.Append("\\textbackslash ");
            }
            else if (Array.IndexOf(specialSymbols, c.ToString()) >= 0)
            {
                sb.Append('\\').Append(c);
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
    
    public static string convertToEmailFormat(this string value)
    {
        var text = value.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (char c in text)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(c);
            if (category != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
    
    public static DateTime setHours(this DateTime value, int hour)
    {
        return new DateTime(value.Year, value.Month, value.Day, hour, 0, 0, DateTimeKind.Utc);
    }

    public static void AppendName(this StringBuilder builder, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            builder.Append(' ').Append(value);
        }
    }
}