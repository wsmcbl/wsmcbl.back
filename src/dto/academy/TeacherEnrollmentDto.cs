using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class TeacherEnrollmentDto
{
    public string teacherId { get; set; } = null!;
    public string enrollmentId { get; set; } = null!;

    public TeacherEnrollmentDto(EnrollmentEntity enrollment)
    {
    }
}