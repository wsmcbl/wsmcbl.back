using wsmcbl.src.model.academy;

namespace wsmcbl.src.model.secretary;

public class DegreeEntity
{
    public string? degreeId { get; set; }
    public string label { get; set; } = null!;
    public string schoolYear { get; set; } = null!;
    public int quantity { get; set; }
    public string modality { get; set; } = null!;

    public ICollection<EnrollmentEntity>? enrollmentList { get; set; }
    public ICollection<SubjectEntity> subjectList { get; set; }

    public DegreeEntity()
    {
        enrollmentList = [];
        subjectList = [];
    }

    public DegreeEntity(DegreeDataEntity degreeData, string schoolYear)
    {
        this.schoolYear = schoolYear;
        label = degreeData.label;
        modality = degreeData.getModalityName();

        subjectList ??= [];
        foreach (var subject in degreeData.subjectList)
        {
            subjectList.Add(new SubjectEntity(subject));
        }
    }

    private readonly string[] typeLabels = ["A", "B", "C", "D", "E", "F", "G", "H", "I"];

    public void createEnrollments(int enrollmentQuantity)
    {
        enrollmentList = [];
        for (var i = 0; i < enrollmentQuantity; i++)
        {
            var enrollment = new EnrollmentEntity(degreeId!, schoolYear, label + " " + typeLabels[i]);
            enrollment.setSubjectList(subjectList);
            enrollmentList.Add(enrollment);
        }
    }

    public void setSubjectList(List<SubjectEntity> subjects)
    {
        subjectList = subjects;
    }
}