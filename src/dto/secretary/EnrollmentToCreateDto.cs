using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.secretary;

public class EnrollmentToCreateDto
{
    [Required] public string gradeId { get; set; } = null!;
    [Required] public int quantity { get; set; }
}