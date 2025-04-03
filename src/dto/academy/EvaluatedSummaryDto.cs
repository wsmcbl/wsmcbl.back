using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EvaluatedSummaryDto
{
    public EvaluationStatsDto initialQuantity { get; set; }
    public EvaluationStatsDto currentQuantity { get; set; }
    public EvaluationStatsDto approved { get; set; }
    public EvaluationStatsDto failedFromOneToTwo { get; set; }
    public EvaluationStatsDto failedFromThreeToMore { get; set; }

    public EvaluatedSummaryDto(List<StudentEntity> parameter, List<StudentEntity> initial)
    {
        initialQuantity = new EvaluationStatsDto(initial);
        currentQuantity = new EvaluationStatsDto(parameter);

        var approvedList = parameter.Where(e => e.passedAllSubjects()).ToList();
        approved = new EvaluationStatsDto(approvedList);
        
        var failedFromOneToTwoList = parameter.Where(e => e.isFailed(1)).ToList();
        failedFromOneToTwo = new EvaluationStatsDto(failedFromOneToTwoList);
        
        var failedFromThreeToMoreList = parameter.Where(e => e.isFailed(2)).ToList();
        failedFromThreeToMore = new EvaluationStatsDto(failedFromThreeToMoreList);
    }
}