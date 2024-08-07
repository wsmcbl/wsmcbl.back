namespace wsmcbl.src.model.academy;

public class EnrollmentEntity
{
    public string? enrollmentId { get; set; }
    public string degreeId { get; set; } = null!;
    public string label { get; set; } = null!;
    public string schoolYear { get; set; } = null!;
    public string section { get; set; } = null!;
    public int capacity { get; set; }
    public int quantity { get; set; }

    public ICollection<StudentEntity>? studentList { get; }
    public ICollection<SubjectEntity>? subjectList { get; private set;}

    public void setSubjectList(List<SubjectEntity> subjects)
    {
        subjectList = subjects;
    }

    public void setSubjectList(ICollection<secretary.SubjectEntity>? subjects)
    {
        if (subjects == null || subjects.Count == 0)
        {
            subjectList = [];
            return;
        }

        subjectList ??= new List<SubjectEntity>();

        foreach (var item in subjects)
        {
            var subject = new SubjectEntity
            {
                subjectId = item.subjectId
            };

            subjectList.Add(subject);
        }
    }

    public void update(EnrollmentEntity enrollment)
    {
        label = enrollment.label;
        section = enrollment.section;
        capacity = enrollment.capacity;
        quantity = enrollment.quantity;
    }
}