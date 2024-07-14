using wsmcbl.src.model.config;

namespace wsmcbl.src.model.academy;

public class TeacherEntity
{
    public string Teacherid { get; set; } = null!;

    public string Userid { get; set; } = null!;

    public string? Enrollmentid { get; set; }

    public virtual EnrollmentEntity? Enrollment { get; set; }

    public virtual ICollection<SubjectEntity> Subjects { get; set; } = new List<SubjectEntity>();

    public virtual UserEntity User { get; set; } = null!;
}