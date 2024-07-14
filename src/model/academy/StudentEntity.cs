namespace wsmcbl.src.model.academy;

public class StudentEntity
{
    public string studentId { get; set; } = null!;

    public string enrollmentId { get; set; } = null!;

    public string schoolYear { get; set; } = null!;

    public bool isApproved { get; set; }

    public EnrollmentEntity enrollment { get; set; } = null!;

    public ICollection<ScoreEntity> scores { get; set; } = new List<ScoreEntity>();

    public secretary.StudentEntity student { get; set; } = null!;
}