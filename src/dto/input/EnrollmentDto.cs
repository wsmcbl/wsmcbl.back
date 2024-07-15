using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.input;

public class EnrollmentDto
{
    [Required] public string? enrollmentId { get; set; }
    [Required] public string? label { get; set; }
    [Required] public string? schoolYear { get; set; }
    [Required] public string? section { get; set; }
    public int capacity { get; set; }
}