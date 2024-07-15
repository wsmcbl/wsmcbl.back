using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.input;

public class TeacherToAssignDto
{
    [Required] public string teacherId { get; set; } = null!;
    [Required] public string subjectId { get; set; } = null!;
    [Required] public string enrollmentId { get; set; } = null!;
}