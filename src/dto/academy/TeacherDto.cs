using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class TeacherDto
{
    public string teacherId { get; set; }
    public string fullName { get; set; }
    public bool isGuide { get; set; }
    public string enrollmentId { get; set; }
    public string enrollmentLabel { get; set; }

    public TeacherDto(TeacherEntity entity)
    {
        teacherId = entity.teacherId!;
        fullName = entity.fullName();
        isGuide = entity.isGuide;

        enrollmentId = string.Empty;
        enrollmentLabel = "Sin matrícula.";

        if (entity.enrollment == null)
        {
            return;
        }
        
        enrollmentId = entity.enrollment.enrollmentId!;
        enrollmentLabel = entity.enrollment.label;
    }
}