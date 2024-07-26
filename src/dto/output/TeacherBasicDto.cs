namespace wsmcbl.src.dto.output;

public class TeacherBasicDto
{
    public string? teacherId { get; set; }
    public string fullName { get; set; } = null!;
    public bool isGuide { get; set; }
}