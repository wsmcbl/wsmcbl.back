using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace wsmcbl.src.dto.input;

public class DateOnlyDto : IBaseDto<DateOnly>
{
    [JsonRequired] public int year { get; set; }
    [JsonRequired] public int month { get; set; }
    [JsonRequired] public int day { get; set; }
    
    public DateOnly toEntity()
    {
        return new DateOnly(year, month, day);
    }

    public static DateOnlyDto init(DateOnly date)
    {
        return new DateOnlyDto
        {
            year = date.Year,
            month = date.Month,
            day = date.Day
        };
    }
}