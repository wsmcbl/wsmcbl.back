namespace wsmcbl.src.model.academy;

public class GradeView
{
    public int id { get; set; }
    public string studentId { get; set; } = null!;
    public int partialId { get; set; }
    public string subjectId { get; set; } = null!;
    public string teacherId { get; set; } = null!;
    public string enrollmentId { get; set; } = null!;
    public string schoolyearId { get; set; } = null!;
    public int semester { get; set; }
    public int partial { get; set; }
    public decimal grade { get; set; }
    public decimal conductGrade { get; set; }
    public string label { get; set; } = null!;
}