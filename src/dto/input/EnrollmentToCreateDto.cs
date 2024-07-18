using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.input;

public class EnrollmentToCreateDto
{
    [Required] public string gradeId { get; set; }
    [Required] public int quantity { get; set; }
}