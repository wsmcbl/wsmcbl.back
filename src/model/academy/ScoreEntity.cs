namespace wsmcbl.src.model.academy;

public class ScoreEntity
{
    public int scoreId { get; set; }
    public string studentId { get; set; } = null!;
    public string subjectId { get; set; } = null!;
    public string enrollmentId { get; set; } = null!;
    public string? label { get; set; }
    public double? score { get; set; }
    
    public ICollection<ScoreItemEntity> scoreItems { get; set; }
}