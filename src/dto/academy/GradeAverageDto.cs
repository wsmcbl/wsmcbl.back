using wsmcbl.src.model.academy;
using wsmcbl.src.utilities;

namespace wsmcbl.src.dto.academy;

public class GradeAverageDto
{
    public int partialId { get; set; }
    public decimal grade { get; set; }
    public string label { get; set; }

    public GradeAverageDto(GradeAverageView parameter)
    {
        partialId = parameter.partialId;
        grade = parameter.grade.round();
        label = parameter.getLabel();
    }
}