using System.ComponentModel.DataAnnotations;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.secretary;

public class SubjectToAssignDto
{
    [Required] public string subjectId { get; set; } = null!;
    [Required] public string? teacherId { get; set; }

    public SubjectToAssignDto()
    {
    }

    public SubjectToAssignDto(SubjectEntity entity)
    {
        subjectId = entity.subjectId;
        teacherId = entity.teacherId;
    }
    
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