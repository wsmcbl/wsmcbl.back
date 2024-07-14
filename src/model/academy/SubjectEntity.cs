namespace wsmcbl.src.model.academy;

public class SubjectEntity
{
    public string subjectId { get; set; } = null!;

    public string baseSubjectId { get; set; } = null!;

    public string teacherId { get; set; } = null!;

    public string enrollmentId { get; set; } = null!;

    public secretary.SubjectEntity baseSubject { get; set; } = null!;

    public ICollection<ScoreEntity> scores { get; set; } = new List<ScoreEntity>();
}