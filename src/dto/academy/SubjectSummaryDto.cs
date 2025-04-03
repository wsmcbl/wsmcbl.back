using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class SubjectSummaryDto
{
    public EvaluationStatsDto approved { get; set; }
    public EvaluationStatsDto failed { get; set; }
    public EvaluationStatsDto notEvaluated { get; set; }

    public SubjectSummaryDto(SubjectPartialEntity parameter)
    {
        var approvedList = parameter.gradeList.Where(e => e.isApproved()).ToList();
        approved = new EvaluationStatsDto(approvedList);
        
        var failedList = parameter.gradeList.Where(e => !e.isApproved()).ToList();
        failed = new EvaluationStatsDto(failedList);
        
        var notEvaluatedList = parameter.gradeList.Where(e => e.isNotEvaluated()).ToList();
        notEvaluated = new EvaluationStatsDto(notEvaluatedList);
    }
}