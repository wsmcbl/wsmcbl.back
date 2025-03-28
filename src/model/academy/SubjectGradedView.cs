namespace wsmcbl.src.model.academy;

public class SubjectGradedView
{
    public int id { get; set; }
    public string subjectId { get; set; } = null!;
    public string teacherId { get; set; } = null!;
    public int partialId { get; set; }
    public int studentCount { get; set; }
    public int gradedStudentCount { get; set; }

    public bool areAllStudentsGraded()
    {
        return studentCount == gradedStudentCount;
    }
}