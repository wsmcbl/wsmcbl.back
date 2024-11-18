using wsmcbl.src.exception;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.model.secretary;

public class DegreeEntity
{
    public string? degreeId { get; set; }
    public string label { get; set; } = null!;
    public string schoolYear { get; set; } = null!;
    public int quantity { get; set; }
    public string educationalLevel { get; set; } = null!;
    public string tag { get; set; } = null!;

    public ICollection<EnrollmentEntity>? enrollmentList { get; set; }
    public ICollection<SubjectEntity> subjectList { get; set; }

    public DegreeEntity()
    {
        enrollmentList = [];
        subjectList = [];
    }

    public int getTag()
    {
        try
        {
            return Convert.ToInt32(tag);
        }
        catch (Exception)
        {
            return 1;
        }
    }

    public DegreeEntity(DegreeDataEntity degreeData, string schoolYear)
    {
        this.schoolYear = schoolYear;
        label = degreeData.label;
        educationalLevel = degreeData.getModalityName();
        tag = degreeData.tag;

        subjectList ??= [];
        foreach (var subject in degreeData.subjectList!)
        {
            subjectList.Add(new SubjectEntity(subject));
        }
    }

    private readonly string[] typeLabels = ["A", "B", "C", "D", "E", "F", "G", "H", "I"];

    public void createEnrollments(int quantityToCreate)
    {
        if(enrollmentList != null && enrollmentList.Count != 0)
        {
            throw new IncorrectDataBadRequestException("DegreeEntity");
        }

        if (quantityToCreate < 1 || quantityToCreate > 7)
        {
            throw new BadRequestException("Quantity invalid. The quantity must be 1 to 7.");
        }

        enrollmentList = [];
        for (var i = 0; i < quantityToCreate; i++)
        {
            var enrollment = new EnrollmentEntity(degreeId!, schoolYear, $"{label} {typeLabels[i]}", $"0{i}");
            enrollment.setSubjectList(subjectList);
            enrollmentList.Add(enrollment);
        }
    }

    public void setSubjectList(List<SubjectEntity> subjects)
    {
        subjectList = subjects;
    }
}