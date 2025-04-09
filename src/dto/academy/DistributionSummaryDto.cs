using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class DistributionSummaryDto
{
    public EvaluationStatsDto AA { get; set; }
    public EvaluationStatsDto AS { get; set; }
    public EvaluationStatsDto AF { get; set; }
    public EvaluationStatsDto AI { get; set; }
    public EvaluationStatsDto quantity { get; set; }
    
    public DistributionSummaryDto(List<StudentEntity> parameter, int partial)
    {
        AA = new EvaluationStatsDto(parameter.Where(e => e.isWithInRange("AA", partial)).ToList());
        AS = new EvaluationStatsDto(parameter.Where(e => e.isWithInRange("AS", partial)).ToList());
        AF = new EvaluationStatsDto(parameter.Where(e => e.isWithInRange("AF", partial)).ToList());
        AI = new EvaluationStatsDto(parameter.Where(e => e.isWithInRange("AI", partial)).ToList());
        
        quantity = new EvaluationStatsDto(parameter);
    }
}