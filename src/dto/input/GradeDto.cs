using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.input;

public class GradeDto
{
    [Required] public int gradeId { get; set; }
    [Required] public string? label { get; set; }
    [Required] public string? schoolYear { get; set; }
    [Required] public string? modality { get; set; }
}