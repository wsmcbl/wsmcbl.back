using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.input;

public class GradeToCreateDto
{
    [Required] public string label { get; set; } = null!;
    [Required] public string schoolYear { get; set; } = null!;
    [Required] public string modality { get; set; } = null!;
    public List<string> subjects { get; set; } = null!;
}