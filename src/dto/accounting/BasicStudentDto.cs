using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class BasicStudentDto
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public string enrollmentLabel { get; set; } = null!;
    public string tutor { get; set; } = null!;

    public BasicStudentDto()
    {
    }

    public BasicStudentDto(StudentEntity entity)
    {
        studentId = entity.studentId!;
        fullName = entity.fullName();
        enrollmentLabel = entity.enrollmentLabel!;
        tutor = entity.tutor;
    }
}