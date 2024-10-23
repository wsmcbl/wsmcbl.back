namespace wsmcbl.src.model.academy;

public class GradeEntity
{
    public int gradeId { get; set; }
    public int subjectPartialId { get; set; }
    public string studentId { get; set; } = null!;
    public string? label { get; set; }
    public double? grade { get; set; }
    public double? conductGrade { get; set; }
}