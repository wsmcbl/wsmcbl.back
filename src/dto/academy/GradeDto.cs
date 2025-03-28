using System.Text.Json.Serialization;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class GradeDto
{
    [JsonRequired] public int gradeId { get; set; }
    public string studentId { get; set; } = null!;
    public decimal grade { get; set; }
    public decimal conductGrade { get; set; } 
    public string label { get; set; } = null!;

    public GradeDto()
    {
    }

    public GradeDto(GradeEntity grade)
    {
        gradeId = grade.gradeId;
        studentId = grade.studentId;
        this.grade = grade.grade ?? 0;
        conductGrade = grade.conductGrade ?? 0;
        label = grade.label?? GradeEntity.getLabelByGrade(0);
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