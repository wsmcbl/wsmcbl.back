using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class BasicTeacherDto
{
    public string? teacherId { get; set; }
    public string fullName { get; set; } = null!;
    public bool isGuide { get; set; }

    public BasicTeacherDto()
    {
    }

    public BasicTeacherDto(TeacherEntity entity)
    {
        teacherId = entity.teacherId;
        fullName = entity.fullName();
        isGuide = entity.isGuide;
    }
}