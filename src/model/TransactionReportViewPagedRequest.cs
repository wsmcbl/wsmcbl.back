using System.Globalization;
using wsmcbl.src.exception;
using wsmcbl.src.utilities;

namespace wsmcbl.src.model;

public class TransactionReportViewPagedRequest : PagedRequest
{
    public string? to { get; set; }
    public string? from { get; set; }

    private DateTime _to;
    public DateTime To() => _to;
    
    private DateTime _from;
    public DateTime From() => _from;

    public void parseRangeToDatetime()
    {
        var dates = parseToDateTime(from!,to!);
        
        _to = dates.to;
        _from = dates.from;
    }
    
    public static (DateTime from, DateTime to) parseToDateTime(string from, string to)
    {
        var startDate = DateTime.ParseExact(from, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        var endDate = DateTime.ParseExact(to, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        if (startDate.Date > endDate.Date)
        {
            throw new BadRequestException("The date range is not valid.");
        }

        startDate = startDate.setHours(6);
        
        endDate = isToday(endDate) ? DateTime.UtcNow :
            endDate.setHours(0).Date.AddDays(1).AddHours(6).AddSeconds(-1);
        
        return (startDate, endDate);
    }
    
    private static bool isToday(DateTime date) => date.Date == DateTime.Today.Date;
}