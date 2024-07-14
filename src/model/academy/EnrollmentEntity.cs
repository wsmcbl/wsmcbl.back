namespace wsmcbl.src.model.academy;

public class EnrollmentEntity
{
    public string enrollmentId { get; set; } = null!;

    public string label { get; set; } = null!;

    public string schoolYear { get; set; } = null!;

    public string section { get; set; } = null!;

    public int capacity { get; set; }

    public int quantity { get; set; }

    public int gradeId { get; set; }

    public ICollection<StudentEntity> students { get; set; }

    public ICollection<SubjectEntity> subjects { get; set; }

    public ICollection<TeacherEntity> teachers { get; set; }
    
    
    public void assignTeacher(string subjectId, TeacherEntity teacher)
    {
    }
}