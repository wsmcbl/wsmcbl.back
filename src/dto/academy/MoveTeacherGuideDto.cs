using System.ComponentModel.DataAnnotations;

namespace wsmcbl.src.dto.academy;

public class MoveTeacherGuideDto
{
    [Required] public string enrollmentId { get; set; } = null!;
    [Required] public string newTeacherId { get; set; } = null!;
}