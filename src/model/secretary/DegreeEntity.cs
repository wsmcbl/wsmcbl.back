using wsmcbl.src.exception;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.model.secretary;

public class DegreeEntity
{
    public string? degreeId { get; set; }
    public string label { get; set; } = null!;
    public string schoolyearId { get; set; } = null!;
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

    public DegreeEntity(DegreeDataEntity degreeData, string schoolyearId)
    {
        this.schoolyearId = schoolyearId;
        label = degreeData.label;
        educationalLevel = degreeData.getModalityName();
        tag = degreeData.tag;

        subjectList ??= [];
        foreach (var subject in degreeData.subjectList!.Where(e => e.isActive))
        {
            subjectList.Add(new SubjectEntity(subject));
        }
    }

    private readonly string[] typeLabels = ["A", "B", "C", "D", "E", "F", "G", "H", "I"];

    private const int MIN_BOUNDARY = 1;
    private const int MAX_BOUNDARY = 7;

    public static bool isValidQuantity(int quantityToCreate)
    {
        return quantityToCreate is >= MIN_BOUNDARY and <= MAX_BOUNDARY;
    }
    
    public void createEnrollments(int quantityToCreate)
    {
        if(enrollmentList != null && enrollmentList.Count != 0)
        {
            throw new IncorrectDataException("Degree", "enrollmentList");
        }

        if (quantityToCreate > MAX_BOUNDARY) quantityToCreate = MAX_BOUNDARY;
        if (quantityToCreate < MIN_BOUNDARY) quantityToCreate = MIN_BOUNDARY;

        enrollmentList = [];
        for (var i = 0; i < quantityToCreate; i++)
        {
            var enrollment = new EnrollmentEntity(degreeId!, schoolyearId, $"{label} {typeLabels[i]}", $"0{i}");
            enrollment.setSubjectList(subjectList);
            enrollmentList.Add(enrollment);
        }
    }

    public async Task saveEnrollments(IEnrollmentDao enrollmentDao)
    {
        if (enrollmentList == null)
        {
            return;
        }
        
        await enrollmentDao.createRange(enrollmentList);
        quantity = enrollmentList.Count;
    }
}