using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class EvaluationStatsDto
{
    public int total { get; set; }
    public int males { get; set; }
    public int females { get; set; }

    public EvaluationStatsDto(int parameter, int males)
    {
        total = parameter;
        this.males = males;
        females = parameter - males;
    }

    public EvaluationStatsDto(List<StudentEntity> list) : this(list.Count, list.Count(e => e.student.sex))
    {
    }
}