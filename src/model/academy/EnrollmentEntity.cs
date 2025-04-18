namespace wsmcbl.src.model.academy;

public class EnrollmentEntity
{
    public string? enrollmentId { get; set; }
    public string? teacherId { get; set; }
    public string degreeId { get; set; } = null!;
    public string label { get; set; } = null!;
    public string tag { get; set; } = null!;
    public string schoolyearId { get; set; } = null!;
    public string section { get; set; } = null!;
    public int capacity { get; set; }
    public int quantity { get; set; }
    public ICollection<StudentEntity>? studentList { get; }
    public ICollection<SubjectEntity>? subjectList { get; private set;}

    public EnrollmentEntity()
    {
        studentList = [];
    }
    
    public EnrollmentEntity(string degreeId, string schoolyearId, string label, string tag)
    {
        this.degreeId = degreeId;
        this.schoolyearId = schoolyearId;
        this.label = label;
        section = "Aula ";
        this.tag = tag;
        teacherId = Const.DefaultTeacherId;
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
                subjectId = item.subjectId!,
                teacherId = Const.DefaultTeacherId
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

    public bool isEnrollmentFull()
    {
        return quantity >= capacity;
    }

    public List<SubjectEntity> getSubjectListByTeacherId(string id)
    {
        return subjectList == null ? [] : subjectList.Where(e => e.teacherId == id).Distinct().ToList();
    }

    public void updateQuantity()
    {
        quantity = studentList!.Count;
    }
}