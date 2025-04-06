using wsmcbl.src.model.academy;
using wsmcbl.src.utilities;

namespace wsmcbl.src.dto.academy;

public class GradeAverageDto
{
    public int partial {get; set;}
    public decimal grade { get; set; }
    public string label { get; set; }

    public GradeAverageDto(GradeAverageView parameter)
    {
        partial = parameter.partial;
        grade = parameter.grade.round();
        label = parameter.getLabel();
    }
}