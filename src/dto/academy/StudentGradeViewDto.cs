using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class StudentGradeViewDto
{
    public string subjectId { get; set; }
    public decimal grade { get; set; }
    public decimal conductGrade { get; set; }
    public string label { get; set; }
    
    public StudentGradeViewDto(GradeView parameter)
    {
        subjectId = parameter.subjectId;
        grade = parameter.grade;
        conductGrade = parameter.conductGrade;
        label = parameter.label;
    }
}