using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.academy;

public class MoveTeacherDto
{
    [Required] public string subjectId { get; set; } = null!;
    [Required] public string enrollmentId { get; set; } = null!;
    [Required] public string teacherId { get; set; } = null!;
}