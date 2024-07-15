using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.input;

public class DateDto
{
    [Required]
    public required int year { get; set; }
    
    [Required]
    public required int month { get; set; }
    
    [Required]
    public required int day { get; set; }
}