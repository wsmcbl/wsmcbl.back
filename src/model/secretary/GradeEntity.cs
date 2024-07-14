using wsmcbl.src.model.academy;

namespace wsmcbl.src.model.secretary;

public class GradeEntity
{
    public int Gradeid { get; set; }

    public string Gradelabel { get; set; } = null!;

    public string Schoolyear { get; set; } = null!;

    public virtual ICollection<EnrollmentEntity> Enrollments { get; set; } = new List<EnrollmentEntity>();

    public virtual SchoolyearEntity SchoolyearNavigation { get; set; } = null!;

    public virtual ICollection<SubjectEntity> subjects { get; set; } = new List<SubjectEntity>();

    public void setSubjects(List<SubjectEntity> list)
    {
        subjects = list;
    }
}