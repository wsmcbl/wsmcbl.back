using wsmcbl.src.model.config;

namespace wsmcbl.src.model.academy;

public class TeacherEntity
{
    public Guid userId { get; set; }
    public string teacherId { get; set; } = null!;
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

    public void forgetEnrollment()
    {
        enrollment = null;
        isGuide = false;
    }
}