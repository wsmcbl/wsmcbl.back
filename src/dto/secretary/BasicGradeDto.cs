using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicGradeDto
{
    public string gradeId { get; set; }
    public string? label { get; set; }
    public string? schoolYear { get; set; }
    public int quantity { get; set; }
    public string? modality { get; set; }

    public BasicGradeDto(DegreeEntity degree)
    {
        gradeId = degree.degreeId!;
        label = degree.label;
        modality = degree.modality;
        quantity = degree.quantity;
        schoolYear = degree.schoolYear;
    }
}