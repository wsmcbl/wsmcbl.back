using wsmcbl.src.model.academy;

namespace wsmcbl.src.dto.academy;

public class BasicStudentDto
{
    public string studentId { get; set; }
    public string fullName { get; set; }

    public BasicStudentDto(StudentEntity entity)
    {
        studentId = entity.studentId!;
        fullName = entity.fullName();
    }
}