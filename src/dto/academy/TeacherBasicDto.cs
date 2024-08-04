namespace wsmcbl.src.dto.academy;

public class TeacherBasicDto
{
    public string? teacherId { get; set; }
    public string fullName { get; set; } = null!;
    public bool isGuide { get; set; }
}