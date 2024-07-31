namespace wsmcbl.src.model.academy;

public class SubjectEntity
{
    public string subjectId { get; set; } = null!;
    public string teacherId { get; set; } = null!;
    public string enrollmentId { get; set; } = null!;

    public secretary.SubjectEntity? secretarySubject { get; set; }
    public ICollection<ScoreEntity>? scores { get; set; }

    public void update(SubjectEntity item)
    {
        if (item.subjectId == subjectId)
        {
            teacherId = item.teacherId;
        }
    }
}