using wsmcbl.src.model.academy;

namespace wsmcbl.src.model.secretary;

public class GradeEntity
{
    public string? gradeId { get; set; }
    public string label { get; set; } = null!;
    public string schoolYear { get; set; } = null!;
    public int quantity { get; private set; }
    public string modality { get; set; } = null!;
    
    public ICollection<EnrollmentEntity>? enrollments { get; set; }
    public ICollection<SubjectEntity>? subjectList { get; set; }
    
    public GradeEntity()
    {
        enrollments = [];
        subjectList = [];
    }

    public GradeEntity(GradeDataEntity gradeData)
    {
        label = gradeData.label;
        modality = gradeData.getModalityName();

        subjectList ??= [];
        foreach (var subject in gradeData.subjectList)
        {
            subjectList.Add(new SubjectEntity(subject));
        }
    }

    public void computeQuantity()
    {
        if(enrollments == null)
            return;
        
        quantity = 0;
        foreach (var item in enrollments!)
        {
            quantity += item.quantity;
        }
    }

    private readonly string[] typeLabels = ["A", "B", "C", "D", "E", "F", "G", "H", "I"];
    
    public void createEnrollments(IEnrollmentDao dao, int enrollmentQuantity)
    {
        for (var i = 0; i < enrollmentQuantity; i++)
        {
            var enrollment = new EnrollmentEntity
            {
                gradeId = gradeId!,
                schoolYear = schoolYear,
                label = label + " " + typeLabels[i]
            };
            enrollment.setSubject(subjectList);
            dao.create(enrollment);
        }
    }

    public void setSubjectList(List<SubjectEntity> subjects)
    {
        subjectList = subjects;
    }
}