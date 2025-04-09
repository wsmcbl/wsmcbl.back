using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EvaluationStatsDto
{
    public int total { get; set; }
    public int males { get; set; }
    public int females { get; set; }

    private EvaluationStatsDto(int parameter, int males)
    {
        total = parameter;
        this.males = males;
        females = parameter - males;
    }
    
    public EvaluationStatsDto(List<model.secretary.StudentEntity> list) : this(list.Count, list.Count(e => e.sex))
    {
    }
    
    public EvaluationStatsDto(List<StudentEntity> list) : this(list.Count, list.Count(e => e.student.sex))
    {
    }

    public EvaluationStatsDto(List<GradeEntity> list) : this(list.Count, list.Count(e => e.student!.sex))
    {
    }
}