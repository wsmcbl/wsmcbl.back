using wsmcbl.src.model.academy;

namespace wsmcbl.src.model.secretary;

public class GradeEntity
{
    public int gradeId { get; private set; }
    public string label { get; private set; } = null!;
    public string schoolYear { get; private set; } = null!;
    public int quantity { get; private set; }
    public string modality { get; private set; } = null!;
    public ICollection<EnrollmentEntity>? enrollments { get; set; }
    public ICollection<SubjectEntity>? subjectList { get; set; }
    
    public GradeEntity()
    {
        enrollments = [];
        subjectList = [];
    }
    
    public void init(string label, string schoolYear, string modality)
    {
        this.label = label;
        this.schoolYear = schoolYear;
        this.modality = modality;
    }

    public async Task setSubjects(ISubjectDao dao, List<string> subjectIdsList)
    {
        if(subjectIdsList.Count == 0)
            return;
        
        var list = await dao.getAll();

        subjectList = new List<SubjectEntity>();
        
        foreach (var id in subjectIdsList)
        {
            var subject = list.Find(e => e.subjectId == id);
            
            if (subject != null && subject.gradeId == gradeId)
            {
                subjectList.Add(subject);
            }
        }
    }
    
    public void computeQuantity()
    {
        quantity = 0;
        foreach (var item in enrollments)
        {
            quantity += item.quantity;
        }
    }

    public void setGradeId(int gradeId)
    {
        this.gradeId = gradeId;
    }
}