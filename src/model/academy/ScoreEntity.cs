namespace wsmcbl.src.model.academy;

public class ScoreEntity
{
    public int scoreId { get; set; }
    public string studentId { get; set; } = null!;
    public string subjectId { get; set; } = null!;
    public double? finalScore { get; set; }
    public string schoolyear { get; set; }
    
    public ICollection<ScoreItemEntity> scoreItems { get; set; }
}