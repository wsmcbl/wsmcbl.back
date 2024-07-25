using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicGradeDto
{
    public string gradeId { get; set; }
    public string? label { get; set; }
    public string? schoolYear { get; set; }
    public int quantity { get; set; }
    public string? modality { get; set; }

    public BasicGradeDto(GradeEntity grade)
    {
        gradeId = grade.gradeId!;
        label = grade.label;
        modality = grade.modality;
        quantity = grade.quantity;
        schoolYear = grade.schoolYear;
    }
}