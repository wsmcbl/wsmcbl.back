using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EnrollmentByTeacherDto
{
    public string teacherId { get; set; } = null!;
    public string enrollmentId { get; set; } = null!;
    public string enrollmentLabel { get; set; } = null!;

    public EnrollmentByTeacherDto()
    {
    }
    
    public EnrollmentByTeacherDto(EnrollmentEntity enrollment)
    {
        teacherId = enrollment.teacherId!;
        enrollmentId = enrollment.enrollmentId!;
        enrollmentLabel = enrollment.label;
    }
}