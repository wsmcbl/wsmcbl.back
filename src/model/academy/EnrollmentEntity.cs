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

    public void assignSubject(SubjectEntity subject)
    {
        subjects.Add(subject);
    }

    public void updateData(string label, string schoolYear, string section, int capacity)
    {
        this.label = label;
        this.schoolYear = schoolYear;
        this.section = section;
        this.capacity = capacity;
    }
}