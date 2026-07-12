namespace wsmcbl.src.dto.academy;

public class SubjectPartialByEnrollmentId
{
    public int subjectPartialId { get; set; }
    public string subjectId { get; set; } = string.Empty;
    public string enrollmentId { get; set; } = string.Empty;
    public int partialId { get; set; }
    public string teacherId { get; set; } = string.Empty;

}