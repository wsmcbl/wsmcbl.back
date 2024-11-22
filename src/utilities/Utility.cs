using System.Text;

namespace wsmcbl.src.utilities;

public static class Utility
{
    public static DateTime toUTC6(this DateTime datetime)
    {
        var timeZoneUTC6 = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(datetime, timeZoneUTC6);
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
}