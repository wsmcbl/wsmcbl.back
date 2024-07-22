using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.input;

public class SubjectEnrollmentDto
{
    [Required] public string subjectId { get; set; } = null!;
    [Required] public string teacherId { get; set; } = null!;
    
    public SubjectEntity toEntity(string enrollmentId)
    {
        return new SubjectEntity
        {
            enrollmentId = enrollmentId,
            subjectId = subjectId,
            teacherId = teacherId
        };
    }
}