using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.input;

public class DateOnlyDto : IBaseDto<DateOnly>
{
    [Required] public required int year { get; set; }
    [Required] public required int month { get; set; }
    [Required] public required int day { get; set; }
    
    public DateOnly toEntity()
    {
        return new DateOnly(year, month, day);
    }
}