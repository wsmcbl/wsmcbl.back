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

    public ICollection<StudentEntity> students { get; set; } = null!;
    public ICollection<SubjectEntity>? subjects { get; private set;}

    public void setSubject(ICollection<secretary.SubjectEntity>? subjectList)
    {
        if (subjectList == null || subjectList.Count == 0)
        {
            return;
        }

        if (subjects == null)
        {
            subjects = new List<SubjectEntity>();
        }

        foreach (var item in subjectList)
        {
            var subject = new SubjectEntity();
            subject.baseSubject = item;
            subjects.Add(subject);
        }
    }
}