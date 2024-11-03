using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class GradesToAddDto
{
    public string studentId { get; set; }
    public double? grade { get; set; }
    public double? conductGrade { get; set; }
    public string? label { get; set; }

    public GradesToAddDto(GradeEntity grade)
    {
        studentId = grade.studentId;
        this.grade = grade.grade;
        conductGrade = grade.conductGrade;
        label = grade.label;
    }
}