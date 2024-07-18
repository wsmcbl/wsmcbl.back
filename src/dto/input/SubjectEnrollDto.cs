using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.input;

public class SubjectEnrollDto
{
    [Required] public string subjectId { get; set; }
    [Required] public string teacherId { get; set; }
    
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