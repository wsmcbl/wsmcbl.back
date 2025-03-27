using wsmcbl.src.exception;
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
        if (grade is < 0 or > 100 || conductGrade is < 0 or > 100)
        {
            throw new IncorrectDataException("grade or conductGrade", "The grades must be between 0 and 100.");
        }
        
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