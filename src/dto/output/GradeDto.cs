using wsmcbl.src.model.academy;
using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.output;

public class GradeDto
{
    public string gradeId { get; set; }
    public string label { get; set; }
    public string schoolYear { get; set; }
    public int quantity { get; set; }
    public string modality { get; set; }
    
    public List<EnrollmentEntity> enrollments { get; set; }

    public GradeDto(GradeEntity grade)
    {
        gradeId = grade.gradeId!;
        label = grade.label;
        schoolYear = grade.schoolYear;
        quantity = grade.quantity;
        modality = grade.modality;
        enrollments = grade.enrollments.ToList();
    }
}