using Newtonsoft.Json;

namespace wsmcbl.src.dto;

public class DateOnlyDto : IBaseDto<DateOnly>
{
    [JsonRequired] public int year { get; set; }
    [JsonRequired] public int month { get; set; }
    [JsonRequired] public int day { get; set; }

    public DateOnly toEntity()
    {
        return new DateOnly(year, month, day);
    }

    public DateOnlyDto()
    {
    }

    public DateOnlyDto(int year, int month, int day)
    {
        this.year = year;
        this.month = month;
        this.day = day;
    }

    public DateOnlyDto(DateOnly date) : this(date.Year, date.Month, date.Day)
    {
    }
}