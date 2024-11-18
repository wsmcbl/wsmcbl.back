using wsmcbl.src.model.secretary;

namespace wsmcbl.src.dto.secretary;

public class BasicDegreeDto
{
    public string degreeId { get; set; }
    public string? label { get; set; }
    public string? schoolYear { get; set; }
    public int quantity { get; set; }
    public int position { get; set; }
    public string educationalLevel { get; set; }

    public BasicDegreeDto(DegreeEntity degree)
    {
        degreeId = degree.degreeId!;
        label = degree.label;
        educationalLevel = degree.educationalLevel;
        quantity = degree.quantity;
        schoolYear = degree.schoolYear;
        position = degree.getTag();
    }
}