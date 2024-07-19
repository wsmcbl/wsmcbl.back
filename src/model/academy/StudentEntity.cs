namespace wsmcbl.src.model.academy;

public class StudentEntity
{
    public string studentId { get; set; } = null!;
    public string? enrollmentId { get; set; }
    public string schoolYear { get; set; } = null!;
    public bool isApproved { get; set; }
    

    public secretary.StudentEntity student { get; set; } = null!;
    public ICollection<ScoreEntity> scores { get; set; } = new List<ScoreEntity>();
}