using System.ComponentModel.DataAnnotations;

namespace wsmcbl.back.dto.input;

public class DateDto
{
    [Required]
    public int year { get; set; }
    
    [Required]
    public int month { get; set; }
    
    [Required]
    public int day { get; set; }
}