using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.input;

public class DateOnlyDto : IBaseDto<DateOnly>
{
    [Required] public int year { get; set; }
    [Required] public int month { get; set; }
    [Required] public int day { get; set; }
    
    public DateOnly toEntity()
    {
        return new DateOnly(year, month, day);
    }

    public DateOnlyDto(DateOnly date)
    {
        year = date.Year;
        month = date.Month;
        day = date.Day;
    }
}