namespace wsmcbl.src.model.academy;

public class GradeAverageView
{
    public int id { get; set; }
    public string studentId { get; set; } = null!;
    public int partialId { get; set; }
    public string enrollmentId { get; set; } = null!;
    public string schoolyearId { get; set; } = null!;
    public int partial { get; set; }
    public decimal grade { get; set; }
    public decimal conductGrade { get; set; }

    public string getLabel()
    {
        return GradeEntity.getLabelByGrade(grade);
    }
}