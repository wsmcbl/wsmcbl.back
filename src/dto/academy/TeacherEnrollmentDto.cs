using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class TeacherEnrollmentDto
{
    public int partialId { get; set; }
    public string enrollmentId { get; set; } = null!;
    public string teacherId { get; set; } = null!;

    public SubjectPartialEntity toEntity()
    {
        return new SubjectPartialEntity
        {
            subjectId = string.Empty,
            partialId = partialId,
            enrollmentId = enrollmentId,
            teacherId = teacherId
        };
    }
}