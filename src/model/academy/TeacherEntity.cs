using wsmcbl.src.model.config;

namespace wsmcbl.src.model.academy;

public class TeacherEntity
{
    public string userId { get; set; } = null!;
    public string teacherId { get; set; } = null!;
    public string? enrollmentId { get; set; }

    
    public UserEntity user { get; set; } = null!;
    public EnrollmentEntity? enrollment { get; set; }
    public ICollection<SubjectEntity> subjects { get; set; }
}