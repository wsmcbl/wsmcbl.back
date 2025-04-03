using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class DistributionSummaryDto
{
    public EvaluationStatsDto AA { get; set; }
    public EvaluationStatsDto AS { get; set; }
    public EvaluationStatsDto AF { get; set; }
    public EvaluationStatsDto AI { get; set; }
    public EvaluationStatsDto quantity { get; set; }
    
    public DistributionSummaryDto(List<StudentEntity> parameter)
    {
        
        AA = new EvaluationStatsDto(parameter.Where(e => e.hasAverageRange(1)));
        AS = new EvaluationStatsDto(parameter.Where(e => e.hasAverageRange(2)));
        AF = new EvaluationStatsDto(parameter.Where(e => e.hasAverageRange(3)));
        AI = new EvaluationStatsDto(parameter.Where(e => e.hasAverageRange(4)));
        
        quantity = new EvaluationStatsDto(parameter);
    }
}