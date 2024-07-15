using wsmcbl.src.model.academy;

namespace wsmcbl.src.model.secretary;

public class GradeEntity
{
    public int gradeId { get; }
    public string label { get; private set; } = null!;
    public string schoolYear { get; private set; } = null!;
    public int quantity { get; private set; }

    public string modality => "sin implementar";

    public void computeQuantity()
    {
        quantity = 0;
        foreach (var item in enrollments)
        {
            quantity += item.quantity;
        }
    }
    
    public ICollection<EnrollmentEntity> enrollments { get; set; } = new List<EnrollmentEntity>();

    public ICollection<SubjectEntity> subjects { get; set; } = new List<SubjectEntity>();

    public void setSubjects(List<SubjectEntity> list)
    {
        /*temporal*/
        subjects = list;
    }
}