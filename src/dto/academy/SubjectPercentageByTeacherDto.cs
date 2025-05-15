using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class SubjectPercentageByTeacherDto
{
    public string subjectId { get; set; }
    public string enrollmentId {get; set;}
    
    public EvaluationStatsDto studentsCount { get; set; }
    public EvaluationStatsDto studentsNotEvaluated { get; set; }
    

    public SubjectPercentageByTeacherDto(SubjectPartialEntity parameter)
    {
        subjectId = parameter.subjectId;
        enrollmentId = parameter.enrollmentId;

        studentsCount = new EvaluationStatsDto(parameter.gradeList.ToList());
        
        var notEvaluatedList = parameter.gradeList.Where(e => e.isNotEvaluated()).ToList();
        studentsNotEvaluated = new EvaluationStatsDto(notEvaluatedList);
    }
}