using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.input;

public class TeacherToAssignDto
{
    [Required] public string? teacherId { get; set; }
    [Required] public string? subjectId { get; set; }
    [Required] public string? enrollmentId { get; set; }
}