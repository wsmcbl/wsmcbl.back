namespace wsmcbl.src.model.academy;

internal static class Utilities
{
    public static string getLabel(double score)
    {
        var label = score >= 60 ? "AF" : "AI";
        label = score is >= 76 and < 90 ? "AS" : "AA";

        return label;
    }
}