using wsmcbl.src.model.academy;

namespace wsmcbl.src.model.secretary;

public class GradeEntity
{
    public int gradeId { get; set; }

    public string label { get; set; } = null!;

    public string schoolYear { get; set; } = null!;

    
    public ICollection<EnrollmentEntity> enrollments { get; set; } = new List<EnrollmentEntity>();

    public ICollection<SubjectEntity> subjects { get; set; } = new List<SubjectEntity>();

    public void setSubjects(List<SubjectEntity> list)
    {
        subjects = list;
    }
}