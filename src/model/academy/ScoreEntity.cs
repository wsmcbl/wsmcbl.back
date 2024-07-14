namespace wsmcbl.src.model.academy;

public class ScoreEntity
{
    public string studentId { get; set; } = null!;

    public string subjectId { get; set; } = null!;

    public string enrollmentId { get; set; } = null!;

    public double? cumulative { get; set; }

    public double? exam { get; set; }

    public double? finalScore { get; set; }
}