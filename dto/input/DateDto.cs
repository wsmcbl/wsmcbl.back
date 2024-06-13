using System.ComponentModel.DataAnnotations;

namespace wsmcbl.back.dto.input;

public class DateDto
{
    public required int year { get; set; }
    
    [Required]
    public required int month { get; set; }
    
    [Required]
    public required int day { get; set; }
}