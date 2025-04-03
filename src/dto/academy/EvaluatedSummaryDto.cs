using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EvaluatedSummaryDto
{
    public EvaluationStatsDto currentQuantity { get; set; }

    public EvaluatedSummaryDto(List<StudentEntity> parameter)
    {
        currentQuantity = new EvaluationStatsDto();
    }
}