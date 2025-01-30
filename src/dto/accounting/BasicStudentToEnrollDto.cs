using wsmcbl.src.model.accounting;

namespace wsmcbl.src.dto.accounting;

public class BasicStudentToEnrollDto
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;

    public BasicStudentToEnrollDto()
    {
    }
    
    public BasicStudentToEnrollDto(StudentEntity student)
    {
        studentId = student.studentId!;
        fullName = student.fullName();
    }
}