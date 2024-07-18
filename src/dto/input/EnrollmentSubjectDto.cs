using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.input;

public class EnrollmentSubjectDto
{
    [Required] public string subjectId { get; set; }
    [Required] public string teacherId { get; set; }
    
    public SubjectEntity toEntity(string enrollmentId)
    {
        return new SubjectEntity
        {
            subjectId = subjectId,
            teacherId = teacherId
        };
    }
}