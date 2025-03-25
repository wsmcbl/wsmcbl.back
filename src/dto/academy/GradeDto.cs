using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class GradeDto
{
    public int gradeId { get; set; }
    public string? studentId { get; set; }
    public decimal? grade { get; set; }
    public decimal? conductGrade { get; set; }
    public string? label { get; set; }

    public GradeDto()
    {
    }

    public GradeDto(GradeEntity grade)
    {
        gradeId = grade.gradeId;
        studentId = grade.studentId;
        this.grade = grade.grade;
        conductGrade = grade.conductGrade;
        label = grade.label;
    }

    public GradeEntity toEntity()
    {
        return new GradeEntity
        {
            gradeId = gradeId,
            studentId = studentId!,
            grade = grade,
            conductGrade = conductGrade,
            label = string.Empty
        };
    }
}