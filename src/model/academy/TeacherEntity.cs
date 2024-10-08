using wsmcbl.src.model.config;

namespace wsmcbl.src.model.academy;

public class TeacherEntity
{
    public string userId { get; set; } = null!;
    public string teacherId { get; set; } = null!;
    public string? enrollmentId { get; set; }
    public bool isGuide { get; set; }
    
    public UserEntity user { get; set; } = null!;
    public EnrollmentEntity? enrollment { get; set; }

    public string fullName()
    {
        return user.fullName();
    }

    public string getEnrollmentLabel()
    {
        return enrollment!.label;
    }

    public void deleteEnrollment()
    {
        enrollmentId = null;
        enrollment = null;
    }
}